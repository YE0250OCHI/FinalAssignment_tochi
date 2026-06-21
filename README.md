# 6/22(月) 研修評価用課題

## DBの作成

``` sql
-- DB作成
CREATE DATABASE TASKS_DATABASE
GO

-- DB接続
USE TASKS_DATABASE
GO

-- テーブル作成
CREATE TABLE TASKS
(
  TASK_ID INT PRIMARY KEY IDENTITY(1,1),
  TASK_NAME NVARCHAR(100) NOT NULL,
  ASSIGNEE NVARCHAR(50) NOT NULL,
  DUE_DATE DATE NOT NULL,
  STATUS NVARCHAR(20) NOT NULL,
  CREATE_DATETIME DATETIME NOT NULL DEFAULT GETDATE(),
  UPDATE_DATETIME DATETIME NOT NULL DEFAULT GETDATE()
);

-- 初期データ作成
INSERT INTO TASKS
(
  TASK_NAME,
  ASSIGNEE,
  DUE_DATE,
  STATUS
)
VALUES
(
  N'基本設計',
  N'越智',
  '2026-06-17',
  N'完了'
),
(
  N'スタブ設計',
  N'大原',
  '2026-06-19',
  N'完了'
),
(
  N'画面レイアウト',
  N'野間',
  '2026-06-19',
  N'進行中'
),
(
  N'サーバー設計',
  N'越智',
  '2026-06-19',
  N'進行中'
),
(
  N'画面作成',
  N'野間',
  '2026-06-24',
  N'未着手'
),
(
  N'サーバーコーディング',
  N'越智',
  '2026-06-24',
  N'未着手'
),
(
  N'スタブコーディング',
  N'大原',
  '2026-06-24',
  N'進行中'
);


```

## タスク管理システム（ASP.NET）

### 要件

- 使用するフレームワーク
  - ASP.NET Core Razor Pages : サーバー＋HTML生成
- 使用するパッケージ：
  - Dapper : DB操作拡張機能
  - Microsoft.Data.SqlClient : SQL Server
- 一覧画面（Index）
  - <table>タグを使って、全件表示を行うこと
  - 担当者名、ステータス名によるフィルタリング機能を設けること
  - フィルタリング処理は、LINQで行うこと
- 編集画面（Edit）
  - 一覧で選択されたタスクの更新が行えること
  - 主キーは変更不可とする
  - 更新後は、一覧画面へ遷移して、変更の反映がされていること

## ログ出力バッチファイル（C#コンソール）

### 要件

- 使用するフレームワーク
  - なし　（C#コンソールアプリケーション）
- 使用するパッケージ：
  - Dapper : DB操作拡張機能
  - Microsoft.Data.SqlClient : SQL Server
- 未完了タスクの抽出
  - 全タスクデータからステータスが完了以外のタスクを抽出して一覧表示する
- 期限切れ警告処理
  - 期限が今日を過ぎているタスクを、Warnで表示すること
- 担当者集計
  - 担当者ごとの未完了タスクの件数を集計して、Infoで出力すること
 
### ログイメージ

``` text
--- 未完了タスク集計 ---
大原|未完了1件|遅れ0件|
越智|未完了2件|遅れ1件|
野間|未完了2件|遅れ1件|
--- 未完了タスク詳細 ---
WARN [TASK: 3] 画面レイアウト 担当：野間 期日：2026-06-19 (3日遅れ)  進捗：進行中
WARN [TASK: 4] サーバー設計 担当：野間 期日：2026-06-19 (3日遅れ)  進捗：進行中
INFO [TASK: 5] 画面作成 担当：野間 期日：2026-06-24  進捗：未着手
INFO [TASK: 6] サーバーコーディング 担当：越智 期日：2026-06-24 進捗：未着手
INFO [TASK: 7] スタブコーディング 担当：大原 期日：2026-06-24 進捗：進行中
```
