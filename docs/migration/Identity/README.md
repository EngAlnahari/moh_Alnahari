# Identity Extraction — Central Migration Memory (Parent Project)

**Parent path:** `D:\EngMohammed\docs\migration\Identity\`  
**Independent Identity repo:** `D:\identity`  
**Identity GitHub:** `https://github.com/Alnahari-Enterprise/identity.git`  
**Last sync date:** 2026-07-29  
**Status:** Isolation analysis complete through **LOOP 05** (approved). LOOP 06+ not started.

This folder is the **parent-project record** of the Identity split.  
Future extractions systems should learn from here — **not** by re-walking `D:\identity` history.

Authoritative in-repo checkpoints inside Identity (for Identity-only work):
- `D:\identity\docs\reports\LOOP_05_OFFICIAL_CHECKPOINT.md` (current)
- `D:\identity\docs\reports\LOOP_02_OFFICIAL_CHECKPOINT.md` (boundaries)

---

## 1) Summary — Identity separation

Goal of the phase: **Extract → Isolate → (later) Restore/Run/Validate** — not redesign.

What was achieved through LOOP 05:

1. Knowledge recovered and validated (conflicts between docs and git recorded).
2. Identity boundaries decided from **code evidence**; Non-Identity quarantined inside the Identity repo.
3. Partial Identity API surface copied from `TestProject/Test` (login/JWT/profile APIs only).
4. Structural gaps mapped: no solution file, single unlinked web project, no Identity DB schema in-repo.
5. Dependencies mapped: Api layer blocked on missing `Test.Data`/`Test.Models`; mixed files documented.
6. Ownership matrix produced (YES / NO / UNKNOWN) without further file moves.

`D:\EngMohammed` source trees were **not modified** by Identity loops (copy-only). Only this documentation tree is added under the parent for process memory.

---

## 2) Loops completed

| Loop | Result | Notes |
|---|---|---|
| LOOP 00 | Context / initialization (project path) | Formal standalone report may be incomplete historically; context carried via governance docs + later loops |
| LOOP 01 | APPROVED | Knowledge Validation — Evidence ≠ Authority |
| LOOP 02 | APPROVED | Boundary discovery + quarantine + selective Api copy |
| LOOP 03 | APPROVED | Migration Gap Report |
| LOOP 04 | APPROVED | Dependency Map Report |
| LOOP 05 | APPROVED | Ownership Matrix |

**Next (not started):** LOOP 06 — Migration Strategy Planning.

---

## 3) Important architectural decisions

1. **Identity Core** keeps auth/user/profile/token/verification-related pieces; **excludes** Tanjez/Land/WorkOffer/Form Builder operational domains.
2. **Manager.cs** is **not** Identity (no User FK) → quarantined in Identity repo.
3. **UserData.cs** filename was misleading (100% business DTOs) → quarantined.
4. **Notifications** are **Shared** (`UserId` + `ContractId`).
5. **UserController** is **Shared/Mixed** (login + contract/work flows) — do not quarantine whole file; split only after migration (or explicit later loop).
6. **Backend API:** only Users + profile controllers + TokenCreate + JWT settings belong to Identity; ~27 business API controllers stay in EngMohammed.
7. **Do not copy `Test.Models`/`Test.Data` yet** — would create a full second model layer; LOOP 06 must choose copy vs retarget to `Domain/Models`.
8. **DbContextFirst** remains Shared in legacy; Identity DB extraction deferred.
9. **No redesign / no secret rotation / no build green-wash** inside analysis loops.

---

## 4) Quarantined / Non-Identity (inside Identity repo)

Physical quarantine path:

`D:\identity\src\Identity\_NonIdentity_Quarantine\` (**96 files**)

Includes (high level):

- Business Presentation Views (TanjezOrder, EarthBorders, WorkOffer, DynamicForms, …) — 20 folders
- `Domain/Reference/*` business lookup models
- `DynamicFormsDbContext`
- Business uploads under wwwroot
- `Manager.cs`, `UserData.cs`, `UpdateStatusDto.cs`

**Also Non-Identity but not quarantined (flagged):**  
`D:\identity\database\` Form Builder databases (`DynamicForms.db`, `dynamic_forms.db`) — decision deferred.

---

## 5) Shared components (keep conscious)

| Item | Note |
|---|---|
| UserController | Identity + business methods mixed |
| Notification model/views | Cross-system |
| BaseApiController / CommonController | Shared helpers |
| User.cs | Identity owned but polluted with business navigations |
| DbContextFirst (legacy) | Identity + business tables together |
| `_Layout` / Home / Shared shell | Generic UI |

---

## 6) Deferred items (for later loops / Owner)

| Item | Target |
|---|---|
| Strategy for Test.Models/Test.Data vs Domain retarget | LOOP 06 |
| `.sln` + ProjectReferences + Api project shape | LOOP 06 |
| Flat vs Presentation dual tree | LOOP 06 / 11 |
| Identity database extraction / migrations | LOOP 07 / 09 |
| Auth wiring (Program/JWT host) | LOOP 09 / 12 |
| NuGet restore/build validation | LOOP 15 |
| Fate of root Form Builder `database/` in Identity repo | Owner + LOOP 07 |
| Remove duplicate IUserModel; Experience→Manager link | Cleanup before real Domain build |
| Split UserController; clean User navigations | Post-migration development |
| Rotate JWT/DB secrets now duplicated in Identity Api appsettings | After migration |

---

## 7) Lessons & rules for extracting the **next** system

1. **Repo reality > documents.** Always inspect files/git before trusting PROJECT_STATE or stage notes.
2. **Evidence ≠ Authority.** Prior AI/Manus reports guide questions; code decides.
3. **Classify before mass-moving.** LOOP-style boundary → gap → dependency → ownership → **then** plan (LOOP 06) → then extract.
4. **Quarantine Non-Identity; do not delete** during migration.
5. **Never copy an entire Backend API project** because auth lives there — take the identity-capable slice only.
6. **Mixed files (Shared/Mixed)** are normal in legacy monoliths; document them; do not force redesign mid-migration.
7. **Blocked dependencies** that imply a new architecture (duplicate models, DbContext split) belong in **planning**, not silent execution.
8. **Keep process memory in the parent** (`docs/migration/<System>/`) so child repos stay product-focused and independent.
9. **One official checkpoint file per major approval gate** inside the child repo; mirror summary here.
10. **Cursor validates; Project Owner approves; Claude executes** within loop scope — no tool invents scope.

---

## 8) Pointers into the Identity repo

| Doc | Path |
|---|---|
| Current checkpoint | `D:\identity\docs\reports\LOOP_05_OFFICIAL_CHECKPOINT.md` |
| Boundary checkpoint | `D:\identity\docs\reports\LOOP_02_OFFICIAL_CHECKPOINT.md` |
| Combined 03–05 report | `D:\identity\docs\reports\LOOP_03_04_05_COMBINED_REPORT.md` |
| Operating loops | `D:\identity\docs\Loops\Identity_Migration_Loops.md` |
| Project state | `D:\identity\PROJECT_STATE.md` |

---

**Do not start LOOP 06 from the parent repo.** LOOP 06 is planned/executed against `D:\identity` only after explicit Owner approval.
