# Subscribe API 仕様書

## ベースURL
`/api/v1/subscribe`

## エンドポイント

### 1. サブスクリプションの作成

- **メソッド**: `POST`
- **パス**: `/create`
- **ヘッダー**:
  - `x-requestId`: Guid (必須)
- **ボディ**: CreateSubscribeCommand (JSON)
  ```json
    {
      "paymentDay": "DateTime",
      "startDay": "DateTime",
      "colorCode": "string",
      "isYear": "bool",
      "categoryAggregateId": "Guid",
      "userAggregateId": "Guid",
      "subscribeName": "string",
      "amount": "decimal",
      "expectedDateOfCancellation": "DateTime?"
    }
  ```
- **レスポンス**:
  - 201 Created: 作成成功
  - 400 Bad Request: リクエストID不正
  - 500 Internal Server Error: サーバーエラー

### 2. サブスクリプションの更新

- **メソッド**: `PUT`
- **パス**: `/update`
- **ヘッダー**:
  - `x-requestId`: Guid (必須)
- **ボディ**: UpdateSubscribeCommand (JSON)
  ``` json
    {
      "subscribeAggregateId": "Guid",
      "paymentDay": "DateTime",
      "startDay": "DateTime",
      "colorCode": "string",
      "isYear": "bool",
      "isActive": "bool",
      "categoryAggregateId": "Guid",
      "userAggregateId": "Guid",
      "subscribeName": "string",
      "amount": "decimal",
      "expectedDateOfCancellation": "DateTime?"
    }
  ```
- **レスポンス**:
  - 201 Created: 更新成功
  - 400 Bad Request: リクエストID不正
  - 500 Internal Server Error: サーバーエラー

### 3. サブスクリプションの削除

- **メソッド**: `PUT`
- **パス**: `/delete`
- **ヘッダー**:
  - `x-requestId`: Guid (必須)
- **ボディ**: DeleteSubscribeCommand (JSON)
  ``` json
    {
      "subscribeAggregateId": "Guid",
      "userAggregateId": "Guid"
    }
  ```
- **レスポンス**:
  - 201 Created: 削除成功
  - 400 Bad Request: リクエストID不正
  - 500 Internal Server Error: サーバーエラー

### 4. ユーザーのサブスクリプション一覧取得

- **メソッド**: `GET`
- **パス**: `/find`
- **ヘッダー**:
  - `x-requestId`: Guid (必須)
- **クエリパラメータ**:
  - `userid`: Guid (必須)
- **レスポンス**:
  - 200 OK: サブスクリプション一覧 (SubscribeAggregateDto の配列)
  - 400 Bad Request: リクエストID不正
- **レスポンスサンプル**
  ``` json
    {
        "subscribeAggregateId": "0a66f984-d8fa-4ead-b1fa-6c848a4238ad",
        "paymentDay": "2023-07-15T00:00:00Z",
        "startDay": "2023-07-01T00:00:00Z",
        "expectedDateOfCancellation": "2024-06-30T00:00:00Z",
        "colorCode": "#JJJ",
        "isYear": true,
        "isActive": true,
        "categoryAggregateId": "d9b8f61d-8b6e-4d2e-b10b-ff3ed8a6e907",
        "userAggregateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "deleteDay": "2024-07-14T16:02:11.061903Z",
        "subscribeItem": {
            "subscribeName": "Normal Subscription",
            "amount": 30
        }
    }
  ```

### 5. 特定のサブスクリプション情報取得

- **メソッド**: `GET`
- **パス**: `/findbyid`
- **ヘッダー**:
  - `x-requestId`: Guid (必須)
- **クエリパラメータ**:
  - `userid`: Guid (必須)
  - `subscribeid`: Guid (必須)
- **レスポンス**:
  - 200 OK: サブスクリプション情報 (SubscribeAggregateDto)
  - 400 Bad Request: リクエストID不正
- **レスポンスサンプル**
  ``` json
    {
        "subscribeAggregateId": "0a66f984-d8fa-4ead-b1fa-6c848a4238ad",
        "paymentDay": "2023-07-15T00:00:00Z",
        "startDay": "2023-07-01T00:00:00Z",
        "expectedDateOfCancellation": "2024-06-30T00:00:00Z",
        "colorCode": "#JJJ",
        "isYear": true,
        "isActive": true,
        "categoryAggregateId": "d9b8f61d-8b6e-4d2e-b10b-ff3ed8a6e907",
        "userAggregateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "deleteDay": "2024-07-14T16:02:11.061903Z",
        "subscribeItem": {
            "subscribeName": "Normal Subscription",
            "amount": 30
        }
    }
  ```
## 注意事項

- すべてのエンドポイントで `x-requestId` ヘッダーが必須です。
- エラーレスポンスには詳細なメッセージが含まれます。
- サーバーエラーの場合、500 Internal Server Error が返されます。

---