# CODEX HHH MASTER TASK - VERSION 2

# HHH System Reverse Engineering & Architecture Documentation Task

## Role

Act as a Senior Software Architect, System Analyst, Business Analyst, and Technical Documentation Engineer.

You are analyzing an existing HTML-based system called HHH.

Your task is NOT software development.

Do NOT:
- rewrite code
- modify source files
- create new application code
- refactor the system

Your mission is complete reverse engineering and professional documentation.

---

# Project Location

Source System:

D:\EngMohammed\hhh


The source contains HTML pages, JavaScript logic, CSS, libraries, and embedded data structures.

---

# Existing Analysis Knowledge Base

You MUST use the existing generated analysis documents as the primary source of understanding.

Location:

D:\EngMohammed\hhh_*analysis*.txt


These documents already contain extracted knowledge from the system.

Do NOT start from zero.

You must combine, compare, and synthesize these documents.

---

# Important Analysis Files

Consider these as major references:

- hhh_system_map_analysis.txt
- hhh_modules_discovery_analysis.txt
- hhh_business_entities_analysis.txt
- hhh_business_rules_analysis.txt
- hhh_data_model_candidates_analysis.txt
- hhh_dependency_relationship_analysis.txt
- hhh_ui_structure_analysis.txt
- hhh_javascript_logic_analysis.txt
- hhh_js_events_analysis.txt
- hhh_json_data_analysis.txt
- hhh_pages_functional_map.txt
- hhh_user_journey_analysis.txt
- hhh_workflows_analysis.txt
- hhh_components_structure_analysis.txt
- hhh_controls_analysis.txt


---

# Documentation Principles

Every discovered item must be classified as:

## FACT

Information directly confirmed from source files.

Example:

"The file AllTemplate.html contains a form with fields X,Y,Z."


## INFERENCE

A logical conclusion based on multiple facts.

Example:

"The system appears to support land surveying workflows."


## UNKNOWN

Information that cannot be confirmed.

Example:

"The database backend is unknown."


Never invent missing information.

---

# Required Final Documentation

Create:

HHH_SYSTEM_REVERSE_ENGINEERING_MASTER_DOCUMENT.md


The document must include:

---

# 1. Executive Summary

Explain:

- What is HHH
- What problem it solves
- General system purpose
- Main capabilities


---

# 2. System Overview

Include:

- System concept
- Architecture overview
- Main pages
- Main components
- Technical characteristics


---

# 3. Complete Page Analysis

For every HTML page:

Document:

- File name
- Purpose
- Main sections
- Forms
- Controls
- User actions
- Dependencies
- Related pages


---

# 4. Functional Map

Create a complete functional map.

Include:

- Modules
- Features
- Sub-features
- Relationships


---

# 5. Business Entities

Extract all business entities.

For each entity:

Include:

- Name
- Description
- Attributes
- Related entities
- Evidence source


Examples:

- Client
- Engineer
- Land
- Document
- Contract
- Offer
- Request
- Work Form


---

# 6. Data Model Candidates

Create a possible logical data model.

Include:

- Entities
- Fields
- Relationships
- Assumptions


Clearly mark:

Confirmed

or

Candidate


---

# 7. User Journeys

Document user journeys.

Examples:

- Creating a request
- Submitting documents
- Creating work forms
- Creating offers
- Contract interaction


For every journey:

Include:

Start point

Steps

Actions

Outputs

---

# 8. Business Workflows

Document workflows.

Include:

- Process steps
- Decisions
- Conditions
- Inputs
- Outputs


---

# 9. UI Architecture

Analyze:

- Screens
- Forms
- Components
- Navigation
- Layout patterns
- Repeated components


---

# 10. JavaScript Behaviour Analysis

Document:

- Functions
- Events
- Dynamic UI behaviour
- Form handling
- Validation
- Data manipulation
- Storage usage


---

# 11. Dependencies

Document:

External libraries:

Example:

- Bootstrap
- jQuery

Internal dependencies:

- Page relationships
- Shared components


---

# 12. Technical Assessment

Provide professional assessment:

Include:

Strengths

Weaknesses

Risks

Technical debt

Modernization opportunities


---

# 13. Future Architecture Recommendations

Suggest possible future architecture.

Do not implement.

Only recommendations.

Include:

- Frontend architecture
- Backend separation
- Database design
- API requirements
- Security considerations


---

# Output Rules

The final document must be:

- Professional
- Detailed
- Structured
- Suitable for software architects
- Suitable for future developers
- Suitable as a reconstruction reference


Do not produce a short summary.

Produce a complete engineering document.

---

# Execution Strategy

Work in phases.

Phase 1:
Understand all existing analysis documents.

Phase 2:
Create system knowledge model.

Phase 3:
Generate master reverse engineering document.

Phase 4:
Review consistency and missing areas.

Phase 5:
Commit final documents to Git repository.


---

# Git Instructions

After creating the document:

Do not modify source files.

Only add documentation files.

Commit message:

"Create HHH system reverse engineering master document"

Push to repository.

---

# Final Objective

Transform the existing HHH HTML system into a complete documented architecture blueprint that can be used later for:

- redevelopment
- modernization
- integration
- migration
- system evolution