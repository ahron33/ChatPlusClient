# ChatPlusClient

A small Windows chat client written in C# (WinForms, .NET Framework). It has no chat server of its own: messages travel through plain text files on a normal web host. The client uploads files over FTP with [FluentFTP](https://github.com/robinrodricks/FluentFTP) and reads the shared chat over HTTP.

<img width="792" height="581" alt="Screenshot_203" src="https://github.com/user-attachments/assets/89c93cf1-02ae-472c-8bbc-e57c65495cd9" />

## Features

- **Nickname screen** shown on first launch. Your nickname is saved on the host, so on later launches the app skips straight to the chat.
- **Chat window** that refreshes about every 500 ms and scrolls to the newest message.
- **Message limits:** 1 to 150 characters. Messages can be in any language; the nickname is limited to English letters.
- **Status line** under the chat that reports problems (too short, too long) and confirms when a message was sent.
- **Input locks while sending**, so you can't send a second message before the first one goes through.
- **Bold nicknames:** your nickname is converted to Unicode bold sans-serif letters when you send a message, for example 𝗗𝗮𝗻.
- **No accounts or passwords for users.** Each computer is identified by its motherboard UUID.

<img width="389" height="428" alt="Screenshot_201" src="https://github.com/user-attachments/assets/9e73a696-ca07-4b05-9b0a-fb1925959764" />


## How it works

Everything lives in a `ChatPlus` folder on the web host:

```
ChatPlus/
├── Chat/
│   └── chat.txt              the shared chat everyone reads
└── Users/
    └── <machine-id>/
        ├── nickname.txt      this user's nickname
        ├── chat.txt          the message this user just sent
        └── status.txt        "Pending" after a message is sent
```

**First launch (NickNameForm)**
1. The app connects over FTP and looks for `Users/<machine-id>/`.
2. If the folder doesn't exist, it creates it with empty `nickname.txt`, `chat.txt` and `status.txt`.
3. If the folder exists and `nickname.txt` already has a name, the nickname screen is skipped and the chat window opens.
4. Otherwise the user enters a nickname (2 to 25 English letters). It is uploaded to `nickname.txt` and the chat window opens.

**Sending a message (MainForm)**
1. The client reads the user's nickname from `nickname.txt` over HTTP.
2. It uploads `Users/<machine-id>/chat.txt` containing the nickname, the time, and the message on a new line.
3. It uploads `Pending` to `status.txt`.
4. A separate relay program (not part of this repository) collects messages from the `Users/` folders and appends them to `Chat/chat.txt`.

**Reading the chat (MainForm)**
- The client downloads `Chat/chat.txt` over HTTP about twice a second (with a cache-busting query string) and updates the chat box only when the text has changed.

## Requirements

- Windows
- Visual Studio with the .NET desktop development workload (the project targets .NET Framework and uses `System.Management`)
- [FluentFTP](https://www.nuget.org/packages/FluentFTP) 55.1.0 (included in `packages/`)
- A web host with FTP access and a public web folder
- ChatPlusServer running against the same host

## Getting started

1. Clone the repository:
   ```
   git clone https://github.com/ahron33/ChatPlusClient.git
   ```
2. Open `ChatPlus.sln` in Visual Studio and restore the NuGet packages.
3. On your host, create `ChatPlus/Chat/` and put an empty `chat.txt` inside it. The `Users/` folder and its files are created by the client the first time it runs.
4. In `MainForm.cs` and `NickNameForm.cs`, replace the FTP host, user, password, and folder path, and the website address used for the HTTP downloads, with your own. Do not commit real credentials to the repository.
5. Build and run.

## Project structure

| Path | What it is |
| --- | --- |
| `ChatPlus/MainForm.cs` | Chat window: sending, receiving, status messages |
| `ChatPlus/NickNameForm.cs` | Start screen: first-run setup and nickname entry |
| `ChatPlus.sln` | Visual Studio solution |
| `packages/` | NuGet packages (FluentFTP) |

## Status

Early version. Some experimental code from earlier attempts is still in the project.
