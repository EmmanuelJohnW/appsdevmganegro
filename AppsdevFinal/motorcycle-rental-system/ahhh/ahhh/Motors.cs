using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ahhh
{
    public partial class Motors : Form
    {
        private List<Dictionary<string, object>> _motorcycles = new List<Dictionary<string, object>>();
        private List<Dictionary<string, object>> _rentals     = new List<Dictionary<string, object>>();

        public Motors()
        {
            InitializeComponent();
        }

        private async void Motors_Load(object sender, EventArgs e)
        {
            lblCurrentUser.Text = $"Welcome, {Session.Username}";

            cmbView.Items.AddRange(new object[] { "All Motorcycles", "Available Only", "My Rentals" });
            cmbView.SelectedIndex = 0;
            cmbView.SelectedIndexChanged += (s, _) => RefreshGrid();

            btnRent.Click        += btnRent_Click;
            btnReturn.Click      += btnReturn_Click;
            btnAddMotor.Click    += btnAddMotor_Click;
            btnEditMotor.Click   += btnEditMotor_Click;
            btnDeleteMotor.Click += btnDeleteMotor_Click;
            btnRefresh.Click     += async (s, _) => await LoadDataAsync();

            await LoadDataAsync();
        }

        // ── Data loading ───────────────────────────────────────────────────────

        private async Task LoadDataAsync()
        {
            btnRefresh.Enabled = false;
            try
            {
                _motorcycles = await SupabaseService.GetMotorcyclesAsync();
                _rentals     = await SupabaseService.GetRentalsAsync();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        private void RefreshGrid()
        {
            string view = cmbView.SelectedItem?.ToString() ?? "All Motorcycles";
            if (view == "My Rentals")
                ShowRentalsView();
            else
                ShowMotorcyclesView(availableOnly: view == "Available Only");
        }

        // ── Grid views ─────────────────────────────────────────────────────────

        private void ShowMotorcyclesView(bool availableOnly)
        {
            var activeRentalByMotor = BuildActiveRentalLookup();

            var table = new DataTable();
            table.Columns.Add("id",            typeof(int));
            table.Columns.Add("Name",          typeof(string));
            table.Columns.Add("Model",         typeof(string));
            table.Columns.Add("Daily Rate (₱)", typeof(string));
            table.Columns.Add("Available",     typeof(string));
            table.Columns.Add("Rented By",     typeof(string));

            foreach (var m in _motorcycles)
            {
                bool isAvail = GetBool(m, "is_available");
                if (availableOnly && !isAvail) continue;

                int id = GetInt(m, "id");
                activeRentalByMotor.TryGetValue(id, out var rental);

                table.Rows.Add(
                    id,
                    GetString(m, "name"),
                    GetString(m, "model"),
                    "₱" + GetString(m, "daily_rate"),
                    isAvail ? "Yes" : "No",
                    rental != null ? GetString(rental, "username") : ""
                );
            }

            dataGridView1.DataSource = table;
            dataGridView1.Columns["id"].Visible = false;

            btnRent.Enabled        = true;
            btnReturn.Enabled      = false;
            btnAddMotor.Enabled    = true;
            btnEditMotor.Enabled   = true;
            btnDeleteMotor.Enabled = true;
        }

        private void ShowRentalsView()
        {
            var motorNames = BuildMotorNameLookup();
            var motorRates = BuildMotorRateLookup();

            var table = new DataTable();
            table.Columns.Add("rental_id",   typeof(int));
            table.Columns.Add("motor_id",    typeof(int));
            table.Columns.Add("Motorcycle",  typeof(string));
            table.Columns.Add("Start Date",  typeof(string));
            table.Columns.Add("End Date",    typeof(string));
            table.Columns.Add("Days",        typeof(string));
            table.Columns.Add("Total (₱)",   typeof(string));
            table.Columns.Add("Returned At", typeof(string));
            table.Columns.Add("Status",      typeof(string));

            foreach (var r in _rentals)
            {
                if (GetString(r, "username") != Session.Username) continue;

                int motorId = GetInt(r, "motorcycle_id");
                motorNames.TryGetValue(motorId, out string motorName);
                motorRates.TryGetValue(motorId, out decimal rate);

                string startStr = GetString(r, "start_date");
                string endStr   = GetString(r, "end_date");

                string daysStr  = "";
                string totalStr = "";
                if (DateTime.TryParse(startStr, out DateTime start) &&
                    DateTime.TryParse(endStr,   out DateTime end))
                {
                    int days = (end.Date - start.Date).Days + 1;
                    daysStr  = days.ToString();
                    if (rate > 0)
                        totalStr = $"₱{days * rate:0.##}";
                }

                table.Rows.Add(
                    GetInt(r, "id"),
                    motorId,
                    motorName ?? "Unknown",
                    startStr,
                    endStr,
                    daysStr,
                    totalStr,
                    FormatDate(GetString(r, "returned_at")),
                    GetString(r, "status")
                );
            }

            dataGridView1.DataSource = table;
            dataGridView1.Columns["rental_id"].Visible = false;
            dataGridView1.Columns["motor_id"].Visible  = false;

            btnRent.Enabled        = false;
            btnReturn.Enabled      = true;
            btnAddMotor.Enabled    = false;
            btnEditMotor.Enabled   = false;
            btnDeleteMotor.Enabled = false;
        }

        // ── Button handlers ────────────────────────────────────────────────────

        private async void btnRent_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a motorcycle to rent.", "No Selection");
                return;
            }

            var row      = dataGridView1.SelectedRows[0];
            int motorId  = (int)row.Cells["id"].Value;
            string name  = row.Cells["Name"].Value?.ToString();
            string avail = row.Cells["Available"].Value?.ToString();

            if (avail != "Yes")
            {
                MessageBox.Show($"\"{name}\" is not available for rent.", "Unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rateStr = row.Cells["Daily Rate (₱)"].Value?.ToString()?.Replace("₱", "").Trim() ?? "0";
            decimal.TryParse(rateStr, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal dailyRate);

            using (var dateForm = new RentalDateForm(name, dailyRate))
            {
                if (dateForm.ShowDialog(this) != DialogResult.OK) return;

                btnRent.Enabled = false;
                try
                {
                    bool ok = await SupabaseService.CreateRentalAsync(
                        motorId, Session.Username, dateForm.StartDate, dateForm.EndDate);
                    if (ok)
                    {
                        MessageBox.Show(
                            $"Successfully rented \"{name}\" for {dateForm.TotalDays} day(s)!", "Rented",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create rental. Please try again.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { btnRent.Enabled = true; }
            }
        }

        private async void btnReturn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rental to return.", "No Selection");
                return;
            }

            var row       = dataGridView1.SelectedRows[0];
            int rentalId  = (int)row.Cells["rental_id"].Value;
            int motorId   = (int)row.Cells["motor_id"].Value;
            string status = row.Cells["Status"].Value?.ToString();
            string motor  = row.Cells["Motorcycle"].Value?.ToString();

            if (status != "active")
            {
                MessageBox.Show("This rental has already been returned.", "Already Returned",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Return \"{motor}\"?", "Confirm Return",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            btnReturn.Enabled = false;
            try
            {
                bool ok = await SupabaseService.ReturnRentalAsync(rentalId, motorId);
                if (ok)
                {
                    MessageBox.Show("Motorcycle returned successfully!", "Returned",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Failed to process return. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { btnReturn.Enabled = true; }
        }

        private void btnAddMotor_Click(object sender, EventArgs e)
        {
            using (var form = new MotorcycleForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    _ = LoadDataAsync();
            }
        }

        private void btnEditMotor_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a motorcycle to edit.", "No Selection");
                return;
            }

            var row      = dataGridView1.SelectedRows[0];
            int id       = (int)row.Cells["id"].Value;
            string name  = row.Cells["Name"].Value?.ToString();
            string model = row.Cells["Model"].Value?.ToString();
            string rateS = row.Cells["Daily Rate (₱)"].Value?.ToString()?.Replace("₱", "");
            decimal.TryParse(rateS, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal rate);

            using (var form = new MotorcycleForm(id, name, model, rate))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    _ = LoadDataAsync();
            }
        }

        private async void btnDeleteMotor_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a motorcycle to delete.", "No Selection");
                return;
            }

            var row      = dataGridView1.SelectedRows[0];
            int id       = (int)row.Cells["id"].Value;
            string name  = row.Cells["Name"].Value?.ToString();
            string avail = row.Cells["Available"].Value?.ToString();

            if (avail == "No")
            {
                MessageBox.Show("Cannot delete a motorcycle that is currently rented.", "Cannot Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Delete \"{name}\"? This cannot be undone.", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                bool ok = await SupabaseService.DeleteMotorcycleAsync(id);
                if (ok)
                {
                    MessageBox.Show($"\"{name}\" deleted.", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Delete failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private Dictionary<int, Dictionary<string, object>> BuildActiveRentalLookup()
        {
            var lookup = new Dictionary<int, Dictionary<string, object>>();
            foreach (var r in _rentals)
            {
                if (GetString(r, "status") != "active") continue;
                int mid = GetInt(r, "motorcycle_id");
                if (!lookup.ContainsKey(mid))
                    lookup[mid] = r;
            }
            return lookup;
        }

        private Dictionary<int, string> BuildMotorNameLookup()
        {
            var lookup = new Dictionary<int, string>();
            foreach (var m in _motorcycles)
                lookup[GetInt(m, "id")] = $"{GetString(m, "name")} {GetString(m, "model")}".Trim();
            return lookup;
        }

        private Dictionary<int, decimal> BuildMotorRateLookup()
        {
            var lookup = new Dictionary<int, decimal>();
            foreach (var m in _motorcycles)
            {
                if (decimal.TryParse(GetString(m, "daily_rate"),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal rate))
                    lookup[GetInt(m, "id")] = rate;
            }
            return lookup;
        }

        private static string FormatDate(string iso)
        {
            if (string.IsNullOrEmpty(iso)) return "";
            if (DateTime.TryParse(iso, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime dt))
                return dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            return iso;
        }

        private static string GetString(Dictionary<string, object> d, string key)
        {
            if (d.TryGetValue(key, out object val) && val != null) return val.ToString();
            return "";
        }

        private static int GetInt(Dictionary<string, object> d, string key)
        {
            if (d.TryGetValue(key, out object val) && val != null) return Convert.ToInt32(val);
            return 0;
        }

        private static bool GetBool(Dictionary<string, object> d, string key)
        {
            if (d.TryGetValue(key, out object val))
            {
                if (val is bool b) return b;
                if (val is long l) return l != 0;
                if (val is int i) return i != 0;
                if (val is string s) return s.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
