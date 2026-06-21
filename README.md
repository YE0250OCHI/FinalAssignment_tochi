# 6/22(月) 研修評価用課題

0. DBの作成

テーブル作成
``` sql
-- DB作成
CREATE DATABASE TASKS
GO

-- DB接続
USE TASKS
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
  N'サーバー設計',
  N'越智',
  '2026-06-22',
  N'完了'
),
(
  N'画面作成',
  N'野間',
  '2026-06-23',
  N'進行中'
),
(
  N'サーバーコーディング',
  N'越智',
  '2026-06-24',
  N'進行中'
),
(
  N'スタブコーディング',
  N'大原',
  '2026-06-24',
  N'進行中'
),
(
  N'単体テスト',
  N'越智',
  '2026-06-25',
  N'未着手'
);


```

1. 
2. 
