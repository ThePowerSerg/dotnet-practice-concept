# Conversation: xUnit and dotnet test CLI Guide

## 1. xUnit CLI Commands

**xUnit** command-line interface is primarily accessed via:
- `xunit.console.exe` (Native Runner for .NET Framework)
- `dotnet xunit` / `xunit-cli` (for .NET Core/.NET 5+)

### xunit.console.exe (Native Runner)
Usage: `xunit.console [configFile] [assemblyFile] [options] [reporter] [resultFormat filename]`

- **Filtering**: `-method "name"`, `-class "name"`, `-namespace "name"`, `-trait "name=value"`, `-notrait "name=value"`.
- **Execution**: `-parallel option` (none, collections, assemblies, all), `-stoponfail`, `-failskips`.
- **Output**: `-xml filename`, `-html filename`, `-json`, `-vsts`, `-teamcity`.
- **Configuration**: `-nologo`, `-nocolor`, `-noautoreporters`, `-debug`, `-diagnostics`.

### dotnet xunit (CLI Tool)
Usage: `dotnet xunit [configFile] [options] [reporter] [resultFormat filename]`

- **Build Options**: `-framework name`, `-configuration name`, `-nobuild`.
- **Parallelization**: `-parallel option`, `-maxthreads count`.
- **Reporting**: `-quiet`, `-verbose`, `-json`, `-appveyor`, `-vsts`, `-teamcity`.
- **Result Formats**: `-xml filename`, `-xmlv1 filename`, `-html filename`, `-nunit filename`, `-junit filename`.

### xunit-cli (Global .NET Core Tool)
Install via: `dotnet tool install -g xunit-cli`
Usage: `xunit [assemblyFile] [additionalArgs]`

### xunit-viewer (Result Visualization)
Usage: `xunit-viewer -r results.xml -o report.html`

---

## 2. dotnet test and Associated Options

The `dotnet test` command is the primary driver for executing unit tests in .NET (xUnit, NUnit, MSTest).

### Core Execution Options
- **Build Control**:
  - `--no-build`: Skips build (implies `--no-restore`).
  - `--no-restore`: Prevents package restore.
  - `-c, --configuration`: Build config (Debug/Release).
  - `-f, --framework`: Target framework (e.g., `net8.0`).
- **Filtering**:
  - `--filter <EXPRESSION>`: Run specific tests (e.g., `"FullyQualifiedName~MyNamespace"`).
  - `-t, --list-tests`: List discovered tests without running.

### Output and Logging
- `--results-directory <DIR>`: Folder for test results.
- `-l, --logger <LOGGER>`:
  - TRX: `--logger "trx;LogFileName=results.trx"`
  - HTML: `--logger "html;LogFileName=results.html"`
  - Console: `--logger "console;verbosity=detailed"`
  - JUnit/NUnit: Requires third-party packages.

### Diagnostics and Code Coverage
- `--collect "Code Coverage"`: Built-in collector (`.coverage` files).
- `--collect "XPlat Code Coverage"`: Coverlet collector (`coverage.cobertura.xml`).
- `--blame`: Identifie   