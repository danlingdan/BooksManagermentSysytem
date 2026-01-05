# 图书馆管理数据库简要说明（SQL Server）

> 适用范围：读者/借书证管理、图书分类与书目管理、馆藏实体与库位管理、借阅记录、罚款记录、编目日志审计。  
> 数据库：`LibraryDB`

---

## 1. 设计目标与范围

本数据库用于实现图书馆核心业务闭环：

- **读者与借书证**：办证、挂失、补办、注销、有效期管理
- **书目与馆藏**：分类（中图法树结构）、书目（ISBN 级）、馆藏实体（条码级）、库位管理
- **借阅业务**：借出、归还、破损记录、逾期说明
- **罚款业务**：原因、金额、支付状态
- **审计追踪**：管理员对分类/库位/书目/实体的操作记录

---

## 2. 模块与表清单

### 2.1 读者与借书证模块
- `readcard`：借书证信息（证号、有效期、状态）
- `reader`：读者信息（与借书证一对一）

### 2.2 借阅与罚款模块
- `borrow_record`：借阅单头（一次借阅事件/交易）
- `bookborrow`：借阅明细（借了哪一本实体书）
- `fine`：罚款记录（原因、金额、支付状态）

### 2.3 书目与馆藏模块
- `BOOK_CATEGORY`：图书分类（中图法分类树）
- `BIBLIOGRAPHY`：书目信息（ISBN 级）
- `AUTHOR`：作者信息
- `BIBLIO_AUTHOR`：书目-作者关联（解决一书多作者）
- `BOOK_ITEM`：馆藏实体（条码级）

### 2.4 运维审计模块
- `catalog_log`：编目日志（操作对象/类型/时间/操作员）

---

## 3. 表结构与关键约束（重点）

> 下文只列关键字段、主外键与核心规则；更详细数据字典可在此基础上扩展。

---

## 3.1 `readcard` 借书证表

**作用**：管理借书证生命周期与有效期。

- **主键**：`cardID`
- **关键字段**
  - `cardID`：证件号（唯一）
  - `startdate`：注册时间
  - `overdate`：到期时间（固定一年有效期）
  - `state`：状态（正常/注销/挂失/补办中）

**规则/约束**
- 证件号格式：`BRW-年份-类别码-顺序号(6位)`
  - 示例：`BRW-2025-1-000123`
  - 类别码：`1=本校学生，2=本校教师，3=校外人员`
- `overdate = startdate + 1年`
- `cardID` 中年份必须与 `startdate` 年份一致

---

## 3.2 `reader` 读者表

**作用**：保存读者身份信息，与借书证一对一绑定。

- **主键**：`cardID`
- **外键**：`cardID` → `readcard(cardID)`
- **关键字段**
  - `readername`：读者姓名
  - `readertype`：读者类别（本校学生/本校教师/校外人员）
  - `unit`：所属单位/学院/组织
  - `number`：学号/工号（本校师生必填；校外人员必须为空）
  - `borrowed_books_info`：冗余摘要（可选，不作为业务强依赖）
  - `borroweddate`：冗余字段（可选，最近借阅日期）
  - `borrow_note`：备注

**规则/约束**
- `readertype` 必须在枚举中
- `number` 规则：
  - 本校学生/教师：`number` 非空
  - 校外人员：`number` 必须为空
- 读者类别与证件号一致性：
  - `readertype` 必须与 `cardID` 的类别码匹配（脚本中按固定位置校验）

---

## 3.3 `borrow_record` 借阅单头表

**作用**：记录一次借阅事件（单头），可包含多本书。

- **主键**：`borrow_record_id`
- **外键**：`cardID` → `reader(cardID)`
- **关键字段**
  - `borrowdate`：借阅时间
  - `overdate`：归还时间（可为空，空=未归还）
  - `bcomplete`：书籍完整度（完好/轻微破损/严重破损）
  - `add_note`：补充说明（损坏、拖延等话术）

---

## 3.4 `bookborrow` 借阅明细表

**作用**：记录“借了哪一本实体书”（条码级明细）。

- **主键**：`bookborrow_id`
- **外键**
  - `borrow_record_id` → `borrow_record(borrow_record_id)`（可选）
  - `cardID` → `reader(cardID)`
  - `bookID` → `BOOK_ITEM(item_barcode)`
- **关键字段**
  - `bookID`：馆藏条码（唯一对应一本实体书）
  - `borrowdate`：借出时间
  - `overdate`：归还时间（空=未归还）
  - `add_note`：补充说明

**规则/约束**
- 时间一致性：`overdate IS NULL OR overdate >= borrowdate`
- 防重复借出（重点）：
  - 对 `bookID` 建立 **过滤唯一索引**：当 `overdate IS NULL` 时，同一本书只能出现一条记录  
  - 目的：防止“未归还又再次借出”

---

## 3.5 `fine` 罚款记录表

**作用**：记录罚款原因、金额、支付状态。

- **主键**：`fine_id`
- **外键**：`cardID` → `reader(cardID)`
- **关键字段**
  - `readername`：姓名快照（历史保留）
  - `reason`：罚款原因（逾期/破损等）
  - `amount`：金额（>0）
  - `fine_status`：已支付/未支付
  - `created_time`：创建时间

---

## 3.6 `BOOK_CATEGORY` 图书分类表（中图法树）

**作用**：以树结构表达中图法分类（如 I → I2 → I24 → I247.5）。

- **主键**：`category_id`
- **外键**：`parent_category_id` → `BOOK_CATEGORY(category_id)`（自引用树）
- **关键字段**
  - `category_code`：分类号（唯一，如 `I247.5`）
  - `category_name`：分类名
  - `Description`：分类定义/依据
  - `create_time` / `update_time`

---

## 3.7 `STORAGE_LOCATION` 库位表（库存位置树）

**作用**：描述图书放置位置、库存类型与容量。

- **主键**：`location_id`
- **外键**：`parent_location_id` → `STORAGE_LOCATION(location_id)`（自引用树，可选）
- **关键字段**
  - `location_code`：库位编码（唯一，如 `1F-A-01-01-02`、`HOT-01`）
  - `location_name`：库位名称
  - `location_type`：库存类型（普通区/热门区/新书区/工具书/待修复等，枚举）
  - `max_capacity` / `current_quantity`：容量与当前库存量
  - `status`：库位状态（正常/停用/维修/已满/整理中，枚举）

**规则/约束**
- `0 <= current_quantity <= max_capacity`

---

## 3.8 `BIBLIOGRAPHY` 书目信息表（ISBN 级）

**作用**：书目是“内容级”记录；同一书目可对应多册实体书。

- **主键**：`bibliography_id`
- **外键**：`category_id` → `BOOK_CATEGORY(category_id)`
- **唯一**：`ISBN`
- **关键字段**
  - `ISBN`：国际标准书号（唯一）
  - `bibliography_name`：书名
  - `publish`：出版社
  - `publish_date`：出版日期
  - `Description`：内容简介
  - `price`：价格（>=0）
  - `create_time`：录入时间

---

## 3.9 `BOOK_ITEM` 馆藏实体表（条码级）

**作用**：每一本实体书都在此表一条记录。

- **主键**：`item_barcode`（馆藏条码）
- **外键**
  - `bibliography_id` → `BIBLIOGRAPHY(bibliography_id)`
  - `location_id` → `STORAGE_LOCATION(location_id)`
- **关键字段**
  - `current_status`：`AVAILABLE/BORROWED/OFF_SHELF/RESERVED`
  - `physical_condition`：`GOOD/DAMAGED/REPAIR`
  - `acquisition_date`：入库日期
  - `status_changed_date`：上次状态修改时间

---

## 3.10 `AUTHOR` 作者表 与 `BIBLIO_AUTHOR` 关联表

### `AUTHOR`
- **主键**：`author_id`
- 字段：`author_name`、`nationality`、`birth_year`、`biography`

### `BIBLIO_AUTHOR`
**作用**：解决“一书多作者”问题。

- **主键**：`relation_id`
- **外键**
  - `bibliography_id` → `BIBLIOGRAPHY`
  - `author_id` → `AUTHOR`
- **关键约束**
  - 同一书目作者不重复：`UNIQUE(bibliography_id, author_id)`
  - 作者顺序唯一：`UNIQUE(bibliography_id, author_order)`
  - `author_order >= 1`（第一作者、第二作者……）

---

## 3.11 `catalog_log` 编目日志表

**作用**：记录管理员对分类、库位、书目、实体的操作痕迹（审计追踪）。

- **主键**：`log_id`
- **关键字段**
  - `target_type`：`CATEGORY/LOCATION/BIBLIOGRAPHY/BOOK_ITEM`
  - `target_id`：对象标识（字符串）
  - `action_type`：新增/删除/更新/分类/上架/下架/状态变更
  - `operator`：操作员（工号/登录名）
  - `action_time`：操作时间
  - `note`：备注

---

## 4. 典型业务流程（数据流转）

### 4.1 办证/注册读者
1. 插入 `readcard`：生成 `cardID`，写入 `startdate/overdate/state`
2. 插入 `reader`：写入个人信息，`cardID` 必须存在于 `readcard`

### 4.2 书目创建与入库上架
1. 若分类不存在：插入 `BOOK_CATEGORY`
2. 插入 `BIBLIOGRAPHY`（ISBN 唯一）
3. 插入 `BOOK_ITEM`（每册实体一个条码，绑定库位）
4. 写 `catalog_log` 记录新增/上架操作

### 4.3 借书
1. 新增 `borrow_record`（单头）
2. 新增 `bookborrow`（明细：bookID + 借出时间，未还 `overdate=NULL`）
3. 更新 `BOOK_ITEM.current_status = BORROWED`
4. 过滤唯一索引确保同一本书“未还不能再借”

### 4.4 还书与产生罚款
1. 更新 `bookborrow.overdate`（归还时间）
2. 更新 `BOOK_ITEM.current_status = AVAILABLE`
3. 如破损/逾期：
   - 在 `borrow_record` 填写 `bcomplete/add_note`
   - 插入 `fine`（金额、原因、支付状态）

---

## 5. 一致性与数据质量控制（总结）

- 借书证：格式、年份匹配、有效期一年、状态枚举
- 读者：类别枚举、学号工号规则、类别与证件号一致
- 借阅：时间合法、实体书未归还不可重复借出（过滤唯一索引）
- 分类/库位：自引用树结构，便于扩展与检索
- 多作者：关联表 + 唯一约束保证“不重复/有序”
- 审计：`catalog_log` 记录关键操作便于追责与回溯

---

## 6. 备注与扩展建议（可选）

- 可新增存储过程：自动生成 `cardID`（按 年份+类别+顺序号递增）
- 可新增触发器/规则：
  - 工具书区（REFERENCE/TOOL_ONLY）禁止外借
  - 逾期自动计算罚金并生成 `fine`
  - 预约（RESERVED）流程与预约书架联动
- 可新增视图：
  - 读者当前未归还清单
  - 书目库存统计（可借/借出/维修/下架）
  - 分类树路径展示（便于前端显示）

---
