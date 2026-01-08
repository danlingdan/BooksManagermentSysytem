using System;
using System.Drawing;
using System.Windows.Forms;
using RecommendationEngine;
using RecommendationEngine.Models;
using RecommendationEngine.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// ML 模型管理控件
    /// 提供模型训练、保存、加载和统计信息展示
    /// </summary>
    public class MLModelManagementControl : UserControl
    {
        private readonly RecommendationFacade _recommendation;

        private GroupBox _grpStatus;
        private Label _lblModelStatus;
        private Label _lblDataStats;
        private Label _lblLastTraining;

        private GroupBox _grpConfig;
        private NumericUpDown _nudIterations;
        private NumericUpDown _nudApproximationRank;
        private NumericUpDown _nudHistoryDays;
        private CheckBox _chkSaveModel;
        private TextBox _txtModelPath;

        private GroupBox _grpActions;
        private Button _btnTrain;
        private Button _btnSave;
        private Button _btnLoad;
        private Button _btnClearCache;
        private ProgressBar _progressBar;
        private Label _lblProgress;

        private GroupBox _grpResults;
        private TextBox _txtResults;

        /// <summary>
        /// 初始化 ML 模型管理控件
        /// </summary>
        public MLModelManagementControl()
        {
            _recommendation = new RecommendationFacade();
            InitializeComponents();
            RefreshStatus();
        }

        private void InitializeComponents()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(15);
            this.AutoScroll = true;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 状态区域
            InitializeStatusGroup();
            mainLayout.Controls.Add(_grpStatus, 0, 0);

            // 配置区域
            InitializeConfigGroup();
            mainLayout.Controls.Add(_grpConfig, 1, 0);
            mainLayout.SetRowSpan(_grpConfig, 2);

            // 操作区域
            InitializeActionsGroup();
            mainLayout.Controls.Add(_grpActions, 0, 1);

            // 结果区域
            InitializeResultsGroup();
            mainLayout.Controls.Add(_grpResults, 0, 2);
            mainLayout.SetColumnSpan(_grpResults, 2);

            this.Controls.Add(mainLayout);
        }

        private void InitializeStatusGroup()
        {
            _grpStatus = new GroupBox
            {
                Text = "模型状态",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 34));

            _lblModelStatus = new Label
            {
                Text = "模型状态：未知",
                AutoSize = true,
                Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold)
            };
            layout.Controls.Add(_lblModelStatus, 0, 0);

            _lblDataStats = new Label
            {
                Text = "训练数据：--",
                AutoSize = true
            };
            layout.Controls.Add(_lblDataStats, 0, 1);

            _lblLastTraining = new Label
            {
                Text = "上次训练：--",
                AutoSize = true
            };
            layout.Controls.Add(_lblLastTraining, 0, 2);

            _grpStatus.Controls.Add(layout);
        }

        private void InitializeConfigGroup()
        {
            _grpConfig = new GroupBox
            {
                Text = "训练配置",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // 迭代次数
            layout.Controls.Add(new Label { Text = "迭代次数：", AutoSize = true, Margin = new Padding(0, 8, 0, 0) }, 0, 0);
            _nudIterations = new NumericUpDown { Minimum = 5, Maximum = 100, Value = 20, Width = 80 };
            layout.Controls.Add(_nudIterations, 1, 0);

            // 近似秩
            layout.Controls.Add(new Label { Text = "近似秩：", AutoSize = true, Margin = new Padding(0, 8, 0, 0) }, 0, 1);
            _nudApproximationRank = new NumericUpDown { Minimum = 2, Maximum = 50, Value = 8, Width = 80 };
            layout.Controls.Add(_nudApproximationRank, 1, 1);

            // 历史天数
            layout.Controls.Add(new Label { Text = "历史天数：", AutoSize = true, Margin = new Padding(0, 8, 0, 0) }, 0, 2);
            _nudHistoryDays = new NumericUpDown { Minimum = 30, Maximum = 1095, Value = 365, Width = 80 };
            layout.Controls.Add(_nudHistoryDays, 1, 2);

            // 保存模型
            _chkSaveModel = new CheckBox { Text = "训练后保存模型", AutoSize = true, Checked = true };
            layout.Controls.Add(_chkSaveModel, 0, 3);
            layout.SetColumnSpan(_chkSaveModel, 2);

            // 模型路径
            layout.Controls.Add(new Label { Text = "模型路径：", AutoSize = true, Margin = new Padding(0, 8, 0, 0) }, 0, 4);
            _txtModelPath = new TextBox
            {
                Text = "ml_model.zip",
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(_txtModelPath, 1, 4);

            _grpConfig.Controls.Add(layout);
        }

        private void InitializeActionsGroup()
        {
            _grpActions = new GroupBox
            {
                Text = "操作",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            _btnTrain = new Button
            {
                Text = "🚀 开始训练",
                Dock = DockStyle.Fill,
                Height = 35,
                Margin = new Padding(3)
            };
            _btnTrain.Click += BtnTrain_Click;
            layout.Controls.Add(_btnTrain, 0, 0);

            _btnLoad = new Button
            {
                Text = "📂 加载模型",
                Dock = DockStyle.Fill,
                Height = 35,
                Margin = new Padding(3)
            };
            _btnLoad.Click += BtnLoad_Click;
            layout.Controls.Add(_btnLoad, 1, 0);

            _btnSave = new Button
            {
                Text = "💾 保存模型",
                Dock = DockStyle.Fill,
                Height = 35,
                Margin = new Padding(3)
            };
            _btnSave.Click += BtnSave_Click;
            layout.Controls.Add(_btnSave, 0, 1);

            _btnClearCache = new Button
            {
                Text = "🗑️ 清除缓存",
                Dock = DockStyle.Fill,
                Height = 35,
                Margin = new Padding(3)
            };
            _btnClearCache.Click += BtnClearCache_Click;
            layout.Controls.Add(_btnClearCache, 1, 1);

            _progressBar = new ProgressBar
            {
                Dock = DockStyle.Fill,
                Visible = false,
                Margin = new Padding(3)
            };
            layout.Controls.Add(_progressBar, 0, 2);
            layout.SetColumnSpan(_progressBar, 2);

            _lblProgress = new Label
            {
                Text = "",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Blue
            };
            layout.Controls.Add(_lblProgress, 0, 3);
            layout.SetColumnSpan(_lblProgress, 2);

            _grpActions.Controls.Add(layout);
        }

        private void InitializeResultsGroup()
        {
            _grpResults = new GroupBox
            {
                Text = "训练结果 / 日志",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            _txtResults = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9)
            };

            _grpResults.Controls.Add(_txtResults);
        }

        /// <summary>
        /// 刷新状态显示
        /// </summary>
        private void RefreshStatus()
        {
            if (_recommendation.IsMLModelTrained)
            {
                _lblModelStatus.Text = "模型状态：✅ 已训练";
                _lblModelStatus.ForeColor = Color.Green;
            }
            else
            {
                _lblModelStatus.Text = "模型状态：⚠️ 未训练";
                _lblModelStatus.ForeColor = Color.Orange;
            }

            try
            {
                var stats = _recommendation.GetMLTrainingDataStatistics(365);
                _lblDataStats.Text = string.Format("可用数据：{0} 条借阅 | {1} 用户 | {2} 书目",
                    stats.Item1, stats.Item2, stats.Item3);
            }
            catch
            {
                _lblDataStats.Text = "可用数据：获取失败";
            }
        }

        private void Log(string message)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            _txtResults.AppendText(string.Format("[{0}] {1}\r\n", timestamp, message));
        }

        #region 事件处理

        private void BtnTrain_Click(object sender, EventArgs e)
        {
            _btnTrain.Enabled = false;
            _progressBar.Visible = true;
            _progressBar.Value = 0;
            Log("开始训练模型...");

            var worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, args) =>
            {
                var config = new MatrixFactorizationConfig
                {
                    NumberOfIterations = (int)_nudIterations.Value,
                    ApproximationRank = (int)_nudApproximationRank.Value,
                    HistoryDays = (int)_nudHistoryDays.Value
                };

                if (_chkSaveModel.Checked && !string.IsNullOrWhiteSpace(_txtModelPath.Text))
                {
                    config.ModelPath = _txtModelPath.Text;
                }

                var result = _recommendation.TrainMLModel(config, (sender2, pargs) =>
                {
                    worker.ReportProgress(pargs.ProgressPercentage, pargs.Message);
                });

                args.Result = result;
            };

            worker.ProgressChanged += (s, pargs) =>
            {
                _progressBar.Value = pargs.ProgressPercentage;
                _lblProgress.Text = pargs.UserState?.ToString() ?? "";
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                _btnTrain.Enabled = true;
                _progressBar.Visible = false;
                _lblProgress.Text = "";

                if (args.Error != null)
                {
                    Log("训练失败：" + args.Error.Message);
                    MessageBox.Show("训练失败：" + args.Error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (args.Result is MatrixFactorizationTrainingResult result)
                {
                    RefreshStatus();

                    if (result.Success)
                    {
                        Log("训练成功！");
                        Log(string.Format("  训练数据：{0} 条", result.TrainingDataCount));
                        Log(string.Format("  用户数：{0}", result.UniqueUserCount));
                        Log(string.Format("  书目数：{0}", result.UniqueBookCount));
                        Log(string.Format("  耗时：{0} ms", result.TrainingTimeMs));

                        if (result.RootMeanSquaredError.HasValue)
                        {
                            Log(string.Format("  RMSE：{0:F4}", result.RootMeanSquaredError.Value));
                        }

                        if (!string.IsNullOrEmpty(result.ModelFilePath))
                        {
                            Log(string.Format("  模型已保存：{0}", result.ModelFilePath));
                        }

                        _lblLastTraining.Text = "上次训练：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        MessageBox.Show("模型训练成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Log("训练失败：" + result.ErrorMessage);
                        MessageBox.Show("训练失败：" + result.ErrorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "ML模型文件|*.zip|所有文件|*.*";
                dialog.Title = "选择模型文件";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (_recommendation.LoadMLModel(dialog.FileName))
                        {
                            RefreshStatus();
                            Log("模型加载成功：" + dialog.FileName);
                            MessageBox.Show("模型加载成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Log("模型加载失败");
                            MessageBox.Show("模型加载失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log("模型加载错误：" + ex.Message);
                        MessageBox.Show("加载失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_recommendation.IsMLModelTrained)
            {
                MessageBox.Show("模型尚未训练，无法保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "ML模型文件|*.zip";
                dialog.Title = "保存模型";
                dialog.FileName = "ml_model.zip";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _recommendation.SaveMLModel(dialog.FileName);
                        Log("模型已保存：" + dialog.FileName);
                        MessageBox.Show("模型保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Log("模型保存失败：" + ex.Message);
                        MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClearCache_Click(object sender, EventArgs e)
        {
            _recommendation.ClearAllCache();
            Log("缓存已清除");
            MessageBox.Show("缓存已清除！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
