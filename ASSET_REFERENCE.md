# External Assets Reference

## Project
EngMohammed Project Repository

## Purpose

This document defines the relationship between the Git repository and the external assets directory.

The purpose is to keep the GitHub repository clean, lightweight, and focused on source code, documentation, configurations, and project knowledge.

Large binary files, databases, AI models, backups, and other heavy assets are intentionally stored outside GitHub.

---

# Repository Location

Main Repository:

D:\EngMohammed


GitHub Repository:

https://github.com/EngAlnahari/moh_Alnahari.git


The repository contains:

- Source code
- HTML prototypes
- Documentation
- Architecture documents
- Project analysis
- Configuration files
- Scripts
- Inventories

---

# External Assets Location

External Assets Directory:

D:\EngMohammed_Assets


This directory is NOT tracked by Git.

It contains large files that are required for some projects but should not be uploaded to GitHub.

---

# Excluded Asset Categories

## 1. Database Files

Location:

D:\EngMohammed_Assets


Files:

- Awqaf33.mdf
- Awqaf33_log.ldf


Description:

SQL Server database files.

Reason for exclusion:

- Large binary files
- Frequently changing internally
- Not suitable for Git version control

---

## 2. Backup Files

Location:

D:\EngMohammed_Assets


Files:

- SmartBusStation1.bak


Description:

Database backup file.

Reason for exclusion:

- Large binary backup
- Used for restoration only
- Should be managed separately from source code

---

## 3. AI Models

Location:

D:\EngMohammed_Assets


Files:

- sam_vit_b_01ec64.pth


Description:

AI model weights.

Reason for exclusion:

- Large machine learning model file
- GitHub repository should contain references and configuration only

---

## 4. Archives and Historical Assets

Location:

D:\EngMohammed_Assets


Examples:

- .rar files
- Archived project packages
- Historical snapshots


Reason:

These are project assets and backups, not source code.

---

# Git Exclusion Rules

The following file types are excluded from Git:

## Development Generated Files
bin/
obj/
.vs/
pycache/

## Databases
*.mdf
*.ldf
*.bak
*.db
*.sqlite
*.sqlite3

## AI Models
*.pth
*.pt
*.onnx
*.ckpt

## Archives
*.rar
*.zip
*.7z

---

# Restoration Procedure

When moving the project to another computer:

1. Clone the Git repository:
git clone https://github.com/EngAlnahari/moh_Alnahari.git

2. Create external assets directory:
D:\EngMohammed_Assets

3. Restore required external assets.

4. Configure applications to reference the external asset location.

---

# Development Rules

## Rule 1

Source code and documentation belong to Git.

## Rule 2

Large binary assets belong to external storage.

## Rule 3

Do not upload database files, backups, AI models, or generated files to GitHub.

## Rule 4

Every external asset must have a documented reference.

## Rule 5

Any developer or AI assistant working on this project must check this document before assuming a missing file.

---

# Current Project State

Repository Status:

- Git repository initialized
- GitHub synchronization completed
- External assets separated
- Large files excluded
- Project inventory documented
- Architecture documentation stored

---

# Maintenance

Whenever a new external asset is created:

1. Move it to:
D:\EngMohammed_Assets

2. Add its description here.

3. Commit the documentation update to Git.
 
