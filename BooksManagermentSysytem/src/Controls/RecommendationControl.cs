using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Services;
using RecommendationEngine;
using RecommendationEngine.Models;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 图书推荐用户控件
    /// 提供热门榜、相似书推荐、个性化推荐功能
    /// </summary>
    public class RecommendationControl : UserControl
    {
        private readonly RecommendationFacade _recommendation;
        private readonly string _currentCardId;

        private TabControl _tabControl;
        private TabPage _tabTrending;
        private TabPage _tabForYou;
        private TabPage _tabSimilar;

        private DataGridView _dgvTrending;
        private DataGridView _dgvForYou;
        private DataGridView _dgvSimilar;

        private ComboBox _cboTrendingPeriod;
        private TextBox _txtBookIdForSimilar;
        private Button _btnSearchSimilar;
        private Button _btnRefresh;

        private Label _lblNoHistory;

        /// <summary>
        /// 初始化推荐控件
        /// </summary>
        public RecommendationControl()
        {
            _recommendation = new RecommendationFacade();
            
            // 修复：通过Instance单例访问CurrentUser
            var authService = AuthenticationService.Instance;
            _currentCardId = authService.CurrentUser != null ? authService.CurrentUser.CardID : null;

            InitializeComponents();
            LoadTrendingData();

            if (!string.IsNullOrEmpty(_currentCardId))
            {
                LoadPersonalizedData();
            }
        }

        private void InitializeComponents()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);

            // 创建主布局
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 顶部工具栏
            var toolbarPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };

            var lblTitle = new Label
            {
                Text = "📚 图书推荐",
                Font = new Font(this.Font.FontFamily, 14, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 5, 20, 0)
            };
            toolbarPanel.Controls.Add(lblTitle);

            _btnRefresh = new Button
            {
                Text = "刷新推荐",
                Width = 100,
                Height = 30
            };
            _btnRefresh.Click += BtnRefresh_Click;
            toolbarPanel.Controls.Add(_btnRefresh);

            mainLayout.Controls.Add(toolbarPanel, 0, 0);

            // 标签页
            _tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // 热门榜标签页
            _tabTrending = new TabPage("🔥 热门榜");
            InitializeTrendingTab();
            _tabControl.TabPages.Add(_tabTrending);

            // 为你推荐标签页
            _tabForYou = new TabPage("💡 为你推荐");
            InitializeForYouTab();
            _tabControl.TabPages.Add(_tabForYou);

            // 相似书推荐标签页
            _tabSimilar = new TabPage("📖 相似书推荐");
            InitializeSimilarTab();
            _tabControl.TabPages.Add(_tabSimilar);

            mainLayout.Controls.Add(_tabControl, 0, 1);

            this.Controls.Add(mainLayout);
        }

        private void InitializeTrendingTab()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 工具栏
            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            var lblPeriod = new Label
            {
                Text = "统计周期：",
                AutoSize = true,
                Margin = new Padding(0, 8, 5, 0)
            };
            toolbar.Controls.Add(lblPeriod);

            _cboTrendingPeriod = new ComboBox
            {
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cboTrendingPeriod.Items.AddRange(new object[] { "本周热门", "本月热门" });
            _cboTrendingPeriod.SelectedIndex = 0;
            _cboTrendingPeriod.SelectedIndexChanged += CboTrendingPeriod_SelectedIndexChanged;
            toolbar.Controls.Add(_cboTrendingPeriod);

            layout.Controls.Add(toolbar, 0, 0);

            // 数据表格
            _dgvTrending = CreateDataGridView();
            layout.Controls.Add(_dgvTrending, 0, 1);

            _tabTrending.Controls.Add(layout);
        }

        private void InitializeForYouTab()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 无历史提示
            _lblNoHistory = new Label
            {
                Text = "您还没有足够的借阅历史，我们无法为您生成个性化推荐。\n\n多借阅一些书籍后，我们将根据您的阅读偏好为您推荐！",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(this.Font.FontFamily, 11),
                ForeColor = Color.Gray,
                Visible = false
            };

            // 数据表格
            _dgvForYou = CreateDataGridView();

            layout.Controls.Add(_dgvForYou, 0, 0);
            layout.Controls.Add(_lblNoHistory, 0, 0);

            _tabForYou.Controls.Add(layout);
        }

        private void InitializeSimilarTab()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 搜索栏
            var searchPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            var lblBookId = new Label
            {
                Text = "书目ID：",
                AutoSize = true,
                Margin = new Padding(0, 8, 5, 0)
            };
            searchPanel.Controls.Add(lblBookId);

            _txtBookIdForSimilar = new TextBox
            {
                Width = 100
            };
            _txtBookIdForSimilar.KeyDown += TxtBookIdForSimilar_KeyDown;
            searchPanel.Controls.Add(_txtBookIdForSimilar);

            _btnSearchSimilar = new Button
            {
                Text = "查找相似书",
                Width = 100,
                Height = 28
            };
            _btnSearchSimilar.Click += BtnSearchSimilar_Click;
            searchPanel.Controls.Add(_btnSearchSimilar);

            var lblHint = new Label
            {
                Text = "（输入书目ID，查看相似图书推荐）",
                AutoSize = true,
                ForeColor = Color.Gray,
                Margin = new Padding(10, 8, 0, 0)
            };
            searchPanel.Controls.Add(lblHint);

            layout.Controls.Add(searchPanel, 0, 0);

            // 数据表格
            _dgvSimilar = CreateDataGridView();
            layout.Controls.Add(_dgvSimilar, 0, 1);

            _tabSimilar.Controls.Add(layout);
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false
            };

            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "BibliographyId", HeaderText = "书目ID", Width = 60, FillWeight = 10 },
                new DataGridViewTextBoxColumn { Name = "BookName", HeaderText = "书名", FillWeight = 25 },
                new DataGridViewTextBoxColumn { Name = "Authors", HeaderText = "作者", FillWeight = 15 },
                new DataGridViewTextBoxColumn { Name = "CategoryName", HeaderText = "分类", FillWeight = 12 },
                new DataGridViewTextBoxColumn { Name = "BorrowCount", HeaderText = "借阅量", Width = 70, FillWeight = 8 },
                new DataGridViewTextBoxColumn { Name = "Score", HeaderText = "推荐度", Width = 70, FillWeight = 8 },
                new DataGridViewTextBoxColumn { Name = "Reason", HeaderText = "推荐理由", FillWeight = 22 }
            });

            dgv.CellDoubleClick += Dgv_CellDoubleClick;

            return dgv;
        }

        private void LoadTrendingData()
        {
            try
            {
                List<RecommendationResult> results;
                if (_cboTrendingPeriod.SelectedIndex == 0)
                {
                    results = _recommendation.GetWeeklyTrending(20);
                }
                else
                {
                    results = _recommendation.GetMonthlyTrending(20);
                }

                BindDataToGrid(_dgvTrending, results);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载热门榜失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonalizedData()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentCardId))
                {
                    _lblNoHistory.Visible = true;
                    _dgvForYou.Visible = false;
                    return;
                }

                if (!_recommendation.HasSufficientHistory(_currentCardId, 3))
                {
                    _lblNoHistory.Visible = true;
                    _dgvForYou.Visible = false;
                    return;
                }

                _lblNoHistory.Visible = false;
                _dgvForYou.Visible = true;

                var results = _recommendation.GetForYou(_currentCardId, 15);
                BindDataToGrid(_dgvForYou, results);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载个性化推荐失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSimilarBooks(int bibliographyId)
        {
            try
            {
                var results = _recommendation.GetSimilarBooks(bibliographyId, 15);
                BindDataToGrid(_dgvSimilar, results);

                if (results.Count == 0)
                {
                    MessageBox.Show("未找到相似图书推荐。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载相似书推荐失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(DataGridView dgv, List<RecommendationResult> results)
        {
            dgv.Rows.Clear();

            foreach (var item in results)
            {
                dgv.Rows.Add(
                    item.BibliographyId,
                    item.BookName,
                    item.Authors,
                    item.CategoryName,
                    item.BorrowCount,
                    string.Format("{0:P0}", item.Score),
                    item.Reason
                );
            }
        }

        #region 事件处理

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            _recommendation.ClearAllCache();
            LoadTrendingData();
            LoadPersonalizedData();
            MessageBox.Show("推荐数据已刷新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CboTrendingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTrendingData();
        }

        private void BtnSearchSimilar_Click(object sender, EventArgs e)
        {
            SearchSimilarBooks();
        }

        private void TxtBookIdForSimilar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchSimilarBooks();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void SearchSimilarBooks()
        {
            int bibliographyId;
            if (!int.TryParse(_txtBookIdForSimilar.Text.Trim(), out bibliographyId))
            {
                MessageBox.Show("请输入有效的书目ID！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadSimilarBooks(bibliographyId);
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var dgv = sender as DataGridView;
            if (dgv == null)
            {
                return;
            }

            var bibliographyId = dgv.Rows[e.RowIndex].Cells["BibliographyId"].Value;
            if (bibliographyId != null)
            {
                // 可以在这里添加跳转到书目详情的逻辑
                MessageBox.Show(string.Format("书目ID: {0}\n\n双击可查看详情或借阅此书。", bibliographyId), 
                    "书目信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary>
        /// 外部调用：显示指定书目的相似推荐
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        public void ShowSimilarBooks(int bibliographyId)
        {
            _tabControl.SelectedTab = _tabSimilar;
            _txtBookIdForSimilar.Text = bibliographyId.ToString();
            LoadSimilarBooks(bibliographyId);
        }
    }
}
