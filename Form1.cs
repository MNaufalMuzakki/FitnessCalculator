using System.Globalization;
using System.Security.Cryptography;

namespace FitnessCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Ambil input dari TextBox
                double beratBadan = Convert.ToDouble(txtWeight.Text);  // dalam kg
                double tinggiBadan = Convert.ToDouble(txtHeight.Text);  // dalam cm
                double usia = Convert.ToDouble(txtAge.Text);  // dalam tahun
                double lingkarPinggang = Convert.ToDouble(txtWaist.Text);  // dalam cm
                double lingkarPinggul = Convert.ToDouble(txtHip.Text);  // dalam cm (untuk wanita)
                double lingkarLeher = Convert.ToDouble(txtNeck.Text);  // dalam cm
                bool isMale = rbtnMale.Checked;  // menentukan jenis kelamin

                // Cek apakah semua input sudah diisi
                if (beratBadan <= 0 || tinggiBadan <= 0 || usia <= 0 || lingkarPinggang <= 0 || lingkarPinggul <= 0 || lingkarLeher <= 0)
                {
                    MessageBox.Show("Semua input harus diisi dengan angka yang valid dan lebih besar dari 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ==== HITUNG BMI ==== 
                double bmi = beratBadan / Math.Pow(tinggiBadan / 100, 2);
                lblBMI.Text = $"Body Mass Index:\n{bmi:F1} kg/m²";

                lblBMICat1.ForeColor = Color.Black;
                lblBMICat1.BackColor = Color.Transparent;

                lblBMICat2.ForeColor = Color.Black;
                lblBMICat2.BackColor = Color.Transparent;

                lblBMICat3.ForeColor = Color.Black;
                lblBMICat3.BackColor = Color.Transparent;

                lblBMICat4.ForeColor = Color.Black;
                lblBMICat4.BackColor = Color.Transparent;

                if (bmi < 18.5) //LABEL 1
                {
                    lblBMICat.Text = "BMI Kategori :\nBerat Badan Kurang";
                    lblBMICat.ForeColor = Color.DarkRed;
                    lblBMI.ForeColor = Color.DarkRed;

                    lblBMICat1.ForeColor = Color.White;
                    lblBMICat1.BackColor = Color.DarkRed;

                    txtBoxBMI.Text = "Kekurangan berat badan dapat menyebabkan gangguan kesehatan seperti anemia, osteoporosis, dan melemahnya sistem imun.";
                }
                else if (bmi <= 24.9) //LABEL 2
                {
                    lblBMICat.Text = "BMI Kategori :\nNormal";
                    lblBMICat.ForeColor = Color.DarkGreen;
                    lblBMI.ForeColor = Color.DarkGreen;

                    lblBMICat2.ForeColor = Color.White;
                    lblBMICat2.BackColor = Color.DarkGreen;

                    txtBoxBMI.Text = "Rentang yang ideal untuk berat badan sehat, dengan risiko minimal terhadap penyakit terkait berat badan.";
                }
                else if (bmi <= 29.9) //LABEL 3
                {
                    lblBMICat.Text = "BMI Kategori :\nBerat Badan Berlebih";
                    lblBMICat.ForeColor = Color.DarkOrange;
                    lblBMI.ForeColor = Color.DarkOrange;

                    lblBMICat3.ForeColor = Color.White;
                    lblBMICat3.BackColor = Color.DarkOrange;

                    txtBoxBMI.Text = "Memiliki kelebihan berat badan meningkatkan risiko penyakit metabolik seperti hipertensi dan diabetes.";
                }
                else
                {
                    lblBMICat.Text = "BMI Kategori :\nObesitas";
                    lblBMICat.ForeColor = Color.DarkRed;
                    lblBMI.ForeColor = Color.DarkRed;

                    lblBMICat4.ForeColor = Color.White;
                    lblBMICat4.BackColor = Color.DarkRed;

                    txtBoxBMI.Text = "Risiko sangat tinggi untuk penyakit kardiovaskular, diabetes tipe 2, gangguan sendi, dan beberapa jenis kanker.";
                }

                // ==== HITUNG BMR ====
                double bmr;
                if (isMale)
                {
                    // Rumus BMR untuk pria
                    bmr = 66 + (13.75 * beratBadan) + (5 * tinggiBadan) - (6.75 * usia);

                }
                else
                {
                    // Rumus BMR untuk wanita
                    bmr = 655 + (9.563 * beratBadan) + (1.850 * tinggiBadan) - (4.676 * usia);

                }
                lblBMR.Text = $"Basal Metabolic Rate:\n{bmr.ToString("N1", new CultureInfo("id-ID"))} Calories";
                lblBMRCalLs.Text = (1.2 * bmr).ToString("N1", new CultureInfo("id-ID"));
                lblBMRCalLr.Text = (1.375 * bmr).ToString("N1", new CultureInfo("id-ID"));
                lblBMRCalMid.Text = (1.55 * bmr).ToString("N1", new CultureInfo("id-ID"));
                lblBMRCalHr.Text = (1.725 * bmr).ToString("N1", new CultureInfo("id-ID"));
                lblBMRCalHs.Text = (1.9 * bmr).ToString("N1", new CultureInfo("id-ID"));

                // ==== HITUNG HRmax ====
                double hrMax = 220 - usia;
                lblHRMax.Text = $"Heart Rate Max:\n{hrMax.ToString("F0"):F1} BPM";

                // ==== HITUNG HRZone ====
                double hrZoneBB = Math.Round(0.5 * hrMax);
                double hrZoneB = Math.Round(0.6 * hrMax);
                double hrZoneT = Math.Round(0.7 * hrMax);
                double hrZoneA = Math.Round(0.8 * hrMax);
                double hrZoneAB = Math.Round(0.9 * hrMax);
                double hrZoneMax = Math.Round(hrMax);
                lblHRZoneBB.Text = $"{hrZoneBB.ToString("F0")} - {hrZoneB.ToString("F0"):F1}";
                lblHRZoneB.Text = $"{hrZoneB.ToString("F0"):F1} - {hrZoneT.ToString("F0"):F1}";
                lblHRZoneT.Text = $"{hrZoneT.ToString("F0"):F1} - {hrZoneA.ToString("F0"):F1}";
                lblHRZoneA.Text = $"{hrZoneA.ToString("F0"):F1} - {hrZoneAB.ToString("F0"):F1}";
                lblHRZoneAB.Text = $"{hrZoneAB.ToString("F0"):F1} - {hrZoneMax.ToString("F0"):F1}";

                // ==== HITUNG BODY FAT PERCENTAGE DENGAN METODE US NAVY ==== 
                double bfp;

                if (isMale)
                {
                    // Rumus BFP untuk pria
                    bfp = (495 / (1.0324 - (0.19077 * Math.Log10(lingkarPinggang - lingkarLeher)) + (0.15456 * Math.Log10(tinggiBadan)))) - 450;
                    lblBFP.Text = $"Body Fat Percentage:\n(Pria) {bfp:F1}%";

                    lblBFPW1.ForeColor = Color.Black;
                    lblBFPW1.BackColor = Color.Transparent;

                    lblBFPW2.ForeColor = Color.Black;
                    lblBFPW2.BackColor = Color.Transparent;

                    lblBFPW3.ForeColor = Color.Black;
                    lblBFPW3.BackColor = Color.Transparent;

                    lblBFPW4.ForeColor = Color.Black;
                    lblBFPW4.BackColor = Color.Transparent;

                    lblBFPW5.ForeColor = Color.Black;
                    lblBFPW5.BackColor = Color.Transparent;

                    lblBFPW6.ForeColor = Color.Black;
                    lblBFPW6.BackColor = Color.Transparent;

                    lblBFPP1.ForeColor = Color.Black;
                    lblBFPP1.BackColor = Color.Transparent;

                    lblBFPP2.ForeColor = Color.Black;
                    lblBFPP2.BackColor = Color.Transparent;

                    lblBFPP3.ForeColor = Color.Black;
                    lblBFPP3.BackColor = Color.Transparent;

                    lblBFPP4.ForeColor = Color.Black;
                    lblBFPP4.BackColor = Color.Transparent;

                    lblBFPP5.ForeColor = Color.Black;
                    lblBFPP5.BackColor = Color.Transparent;

                    lblBFPP6.ForeColor = Color.Black;
                    lblBFPP6.BackColor = Color.Transparent;

                    if (bfp < 2)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nKekurangan lemak esensial ";
                        lblBFPCat.ForeColor = Color.DarkRed;
                        lblBFP.ForeColor = Color.DarkRed;

                        lblBFPP1.ForeColor = Color.White;
                        lblBFPP1.BackColor = Color.DarkRed;

                        txtBoxBFP.Text = "Keterangan BFP: Lemak tubuh terlalu rendah untuk fungsi vital. " +
                            "Berbahaya bagi kesehatan, menyebabkan gangguan hormon, kerusakan organ, " +
                            "dan masalah regulasi suhu tubuh.";
                    }

                    else if (bfp <= 5)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nLemak Esensial";
                        lblBFPCat.ForeColor = Color.DarkBlue;

                        lblBFPP2.ForeColor = Color.White;
                        lblBFPP2.BackColor = Color.DarkBlue;

                        txtBoxBFP.Text = "Lemak minimum untuk fungsi fisiologis, " +
                            "seperti regulasi hormon dan perlindungan organ. " +
                            "Angka di bawah ini berbahaya untuk kesehatan jangka panjang.";
                    }

                    else if (bfp <= 14)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nAtlet";
                        lblBFPCat.ForeColor = Color.DarkGreen;
                        lblBFP.ForeColor = Color.DarkGreen;

                        lblBFPP3.ForeColor = Color.White;
                        lblBFPP3.BackColor = Color.DarkGreen;

                        txtBoxBFP.Text = "Lemak tubuh optimal untuk pria dengan aktivitas fisik tinggi seperti atlet profesional. " +
                            "Memberikan kekuatan dan daya tahan tanpa berlebihan.";
                    }

                    else if (bfp <= 17)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nKebugaran";
                        lblBFPCat.ForeColor = Color.DarkGreen;
                        lblBFP.ForeColor = Color.DarkGreen;

                        lblBFPP4.ForeColor = Color.White;
                        lblBFPP4.BackColor = Color.DarkGreen;

                        txtBoxBFP.Text = "Kategori sehat untuk pria yang menjaga aktivitas fisik dan pola makan teratur. " +
                            "Ideal untuk kesehatan jangka panjang.";
                    }

                    else if (bfp <= 24)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nRata-rata";
                        lblBFPCat.ForeColor = Color.DarkOrange;
                        lblBFP.ForeColor = Color.DarkOrange;

                        lblBFPP5.ForeColor = Color.White;
                        lblBFPP5.BackColor = Color.DarkOrange;

                        txtBoxBFP.Text = "Tingkat lemak tubuh yang umum ditemukan pada pria dengan aktivitas fisik sedang. " +
                            "Masih tergolong sehat tetapi risiko obesitas mulai meningkat.";
                    }

                    else
                    {
                        lblBFPCat.Text = "BFP Kategori :\nObesitas";
                        lblBFPCat.ForeColor = Color.DarkRed;
                        lblBFP.ForeColor = Color.DarkRed;

                        lblBFPP6.ForeColor = Color.White;
                        lblBFPP6.BackColor = Color.DarkRed;

                        txtBoxBFP.Text = "Tingkat lemak tubuh yang signifikan, meningkatkan risiko masalah kesehatan serius " +
                        "seperti penyakit kardiovaskular, hipertensi, dan gangguan metabolik.";
                    }
                }
                else
                {
                    // Rumus BFP untuk wanita
                    bfp = (495 / (1.29579 - (0.35004 * Math.Log10(lingkarPinggang + lingkarPinggul - lingkarLeher)) + (0.22100 * Math.Log10(tinggiBadan)))) - 450;
                    lblBFP.Text = $"Body Fat Percentage:\n(Wanita) {bfp:F1}%";

                    lblBFPW1.ForeColor = Color.Black;
                    lblBFPW1.BackColor = Color.Transparent;

                    lblBFPW2.ForeColor = Color.Black;
                    lblBFPW2.BackColor = Color.Transparent;

                    lblBFPW3.ForeColor = Color.Black;
                    lblBFPW3.BackColor = Color.Transparent;

                    lblBFPW4.ForeColor = Color.Black;
                    lblBFPW4.BackColor = Color.Transparent;

                    lblBFPW5.ForeColor = Color.Black;
                    lblBFPW5.BackColor = Color.Transparent;

                    lblBFPW6.ForeColor = Color.Black;
                    lblBFPW6.BackColor = Color.Transparent;

                    lblBFPP1.ForeColor = Color.Black;
                    lblBFPP1.BackColor = Color.Transparent;

                    lblBFPP2.ForeColor = Color.Black;
                    lblBFPP2.BackColor = Color.Transparent;

                    lblBFPP3.ForeColor = Color.Black;
                    lblBFPP3.BackColor = Color.Transparent;

                    lblBFPP4.ForeColor = Color.Black;
                    lblBFPP4.BackColor = Color.Transparent;

                    lblBFPP5.ForeColor = Color.Black;
                    lblBFPP5.BackColor = Color.Transparent;

                    lblBFPP6.ForeColor = Color.Black;
                    lblBFPP6.BackColor = Color.Transparent;

                    if (bfp < 10)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nKekurangan lemak esensial ";
                        lblBFPCat.ForeColor = Color.DarkRed;
                        lblBFP.ForeColor = Color.DarkRed;

                        lblBFPW1.ForeColor = Color.White;
                        lblBFPW1.BackColor = Color.DarkRed;

                        txtBoxBFP.Text = "Lemak tubuh terlalu rendah untuk fungsi vital. " +
                            "Berbahaya bagi kesehatan, menyebabkan gangguan hormon, kerusakan organ, dan masalah regulasi suhu tubuh.";
                    }

                    else if (bfp <= 13)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nLemak Esensial";
                        lblBFPCat.ForeColor = Color.DarkBlue;
                        lblBFP.ForeColor = Color.DarkBlue;

                        lblBFPW2.ForeColor = Color.White;
                        lblBFPW2.BackColor = Color.DarkBlue;

                        txtBoxBFP.Text = "Lemak tubuh minimal yang diperlukan untuk fungsi vital seperti produksi hormon, " +
                            "isolasi suhu tubuh, dan perlindungan organ. " +
                            "Kekurangan lemak esensial dapat menyebabkan gangguan hormon dan masalah kesehatan serius.";
                    }

                    else if (bfp <= 20)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nAtlet";
                        lblBFPCat.ForeColor = Color.DarkGreen;
                        lblBFP.ForeColor = Color.DarkGreen;

                        lblBFPW3.ForeColor = Color.White;
                        lblBFPW3.BackColor = Color.DarkGreen;

                        txtBoxBFP.Text = "Tingkat lemak tubuh yang optimal untuk kinerja fisik tinggi. " +
                            "Biasanya dimiliki oleh wanita yang aktif secara fisik seperti atlet atau pelatih kebugaran.";
                    }

                    else if (bfp <= 24)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nKebugaran";
                        lblBFPCat.ForeColor = Color.DarkGreen;

                        lblBFPW4.ForeColor = Color.White;
                        lblBFPW4.BackColor = Color.DarkGreen;

                        txtBoxBFP.Text = "Lemak tubuh sehat untuk wanita dengan gaya hidup aktif tetapi tidak seintensif atlet. " +
                            "Kategori ini dianggap sangat baik untuk kesehatan.";
                    }


                    else if (bfp <= 31)
                    {
                        lblBFPCat.Text = "BFP Kategori :\nRata-rata";
                        lblBFPCat.ForeColor = Color.DarkOrange;
                        lblBFP.ForeColor = Color.DarkOrange;

                        lblBFPW5.ForeColor = Color.White;
                        lblBFPW5.BackColor = Color.DarkOrange;

                        txtBoxBFP.Text = "Tingkat lemak tubuh umum yang ditemukan pada wanita yang memiliki aktivitas fisik sedang hingga rendah. " +
                            "Masih dianggap sehat tetapi mendekati risiko obesitas.";
                    }

                    else
                    {
                        lblBFPCat.Text = "BFP Kategori :\nObesitas";
                        lblBFPCat.ForeColor = Color.DarkRed;
                        lblBFP.ForeColor = Color.DarkRed;

                        lblBFPW6.ForeColor = Color.White;
                        lblBFPW6.BackColor = Color.DarkRed;

                        txtBoxBFP.Text = "Tingkat lemak tubuh yang berisiko tinggi untuk penyakit metabolik " +
                        "seperti diabetes tipe 2, penyakit jantung, dan hipertensi.";
                    }

                }



            }
            catch (FormatException)
            {
                MessageBox.Show("Pastikan semua input berisi angka yang valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFemale.Checked) // Jika rbtnFemale diaktifkan
            {
                txtHip.Enabled = true;
                if (txtHip.Text == "1")
                {
                    txtHip.Text = "";
                }
                else
                {
                    txtHip.Enabled = true;
                }
            }

        }
        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMale.Checked) // Jika rbtnMale diaktifkan
            {
                txtHip.Enabled = false;

                if (txtHip.Text == "")
                {
                    txtHip.Text = "1"; // Set default lingkar pinggang ke 1
                }
                else
                {
                    txtHip.Enabled = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Reset semua input
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtAge.Text = "";
            txtWaist.Text = "";
            txtHip.Text = "";
            txtNeck.Text = "";

            // Reset pilihan gender
            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;

            // Disable txtHip
            txtHip.Enabled = false;
        }

        private void btnClearH_Click(object sender, EventArgs e)
        {
            // Reset hasil
            lblBMI.Text = "";
            lblBMICat.Text = "";
            lblBMR.Text = "";
            lblBMRCalLs.Text = "";
            lblBMRCalLr.Text = "";
            lblBMRCalMid.Text = "";
            lblBMRCalHr.Text = "";
            lblBMRCalHs.Text = "";
            lblHRMax.Text = "";
            lblHRZoneBB.Text = "";
            lblHRZoneB.Text = "";
            lblHRZoneT.Text = "";
            lblHRZoneA.Text = "";
            lblHRZoneAB.Text = "";
            lblBFP.Text = "";
            lblBFPCat.Text = "";
            txtBoxBFP.Text = "";
            txtBoxBMI.Text = "";

            lblBMICat1.ForeColor = Color.Black;
            lblBMICat1.BackColor = Color.Transparent;

            lblBMICat2.ForeColor = Color.Black;
            lblBMICat2.BackColor = Color.Transparent;

            lblBMICat3.ForeColor = Color.Black;
            lblBMICat3.BackColor = Color.Transparent;

            lblBMICat4.ForeColor = Color.Black;
            lblBMICat4.BackColor = Color.Transparent;

            lblBFPP1.ForeColor = Color.Black;
            lblBFPP1.BackColor = Color.Transparent;

            lblBFPP2.ForeColor = Color.Black;
            lblBFPP2.BackColor = Color.Transparent;

            lblBFPP3.ForeColor = Color.Black;
            lblBFPP3.BackColor = Color.Transparent;

            lblBFPP4.ForeColor = Color.Black;
            lblBFPP4.BackColor = Color.Transparent;

            lblBFPP5.ForeColor = Color.Black;
            lblBFPP5.BackColor = Color.Transparent;

            lblBFPP6.ForeColor = Color.Black;
            lblBFPP6.BackColor = Color.Transparent;

            lblBFPW1.ForeColor = Color.Black;
            lblBFPW1.BackColor = Color.Transparent;

            lblBFPW2.ForeColor = Color.Black;
            lblBFPW2.BackColor = Color.Transparent;

            lblBFPW3.ForeColor = Color.Black;
            lblBFPW3.BackColor = Color.Transparent;

            lblBFPW4.ForeColor = Color.Black;
            lblBFPW4.BackColor = Color.Transparent;

            lblBFPW5.ForeColor = Color.Black;
            lblBFPW5.BackColor = Color.Transparent;

            lblBFPW6.ForeColor = Color.Black;
            lblBFPW6.BackColor = Color.Transparent;
        }
    }
}


