<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head>
    <title>Service Unavailable</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f4f4f4;
        }

        .message-box {
            text-align: center;
            background: #fff;
            padding: 40px;
            border: 1px solid #ccc;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            border-radius: 8px;
        }

        h2 {
            color: #333;
            margin-bottom: 10px;
        }

        p {
            color: #666;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <div class="message-box">
        <h2>We’re Working on It</h2>
        <p>The service is temporarily unavailable due to a network connectivity issue.<br />
        We are working on it and it will be back soon.</p>
    </div>
</body>
</html>