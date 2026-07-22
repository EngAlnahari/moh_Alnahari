# Mahwar / Tanjez Project Architecture

## 1. Project Purpose

This repository contains the current development assets of the Mahwar ecosystem prototype and the Tanjez platform.

The repository represents the engineering workspace containing:
- Existing applications
- Experimental prototypes
- AI and GIS related components
- Documentation
- Database assets

---

# 2. Repository Structure

## Mntry_Awqaf

Main ASP.NET Core MVC application.

Purpose:
- Current Tanjez system implementation.
- Contains controllers, models, views, services and frontend resources.

Important folders:

Controllers:
Business and API controllers.

Models:
Domain entities and data models.

Views:
MVC user interfaces.

wwwroot:
Frontend resources.

---

## TestProject

Testing and development version.

Purpose:
- Experiments
- API testing
- Database testing
- Feature validation

---

## LandClassificationProject

AI/GIS related project.

Purpose:
- Land classification experiments.
- Image analysis.
- Machine learning preparation.

---

## hhh

HTML prototypes and interface experiments.

Purpose:
- UI concepts
- Form experiments
- Prototype screens

---

# 3. Database Assets

Database files exist locally:

- Awqaf33.mdf
- Awqaf33_log.ldf
- SmartBusStation1.bak

These are development assets.

Do not modify or delete without approval.

---

# 4. Repository Rules

## Do not commit:

- bin folders
- obj folders
- build outputs
- temporary files
- local databases unless explicitly required
- AI model weights

---

# 5. Development Principle

This repository is considered the engineering baseline.

Before changing architecture:

1. Understand existing code.
2. Document impact.
3. Preserve existing functionality.
4. Avoid destructive changes.

---

# 6. AI Assistant Instructions

Any AI assistant working on this repository must:

- Analyze before modifying.
- Ask before deleting.
- Preserve existing architecture.
- Explain proposed changes.
- Avoid rewriting working systems without approval.
