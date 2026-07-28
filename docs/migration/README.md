# Central Migration Registry (Parent Project)

**Location:** `D:\EngMohammed\docs\migration\`  
**Purpose:** Preserve extraction/isolation lessons for **all** future system splits, without requiring AI tools or humans to dig through each child repository (e.g. `D:\identity`) to recover process memory.

## Systems

| System | Folder | Independent repo (if any) |
|---|---|---|
| Identity | `Identity/` | `D:\identity` → `https://github.com/Alnahari-Enterprise/identity.git` |

When starting a **new** system extraction (Land, Tanjez, Form Builder, etc.):

1. Read this registry index.
2. Read `Identity/` for proven rules and anti-patterns.
3. Create a new sibling folder under `docs/migration/<SystemName>/`.
4. Keep the child product repo independent; keep **process memory** here.

Do **not** treat child-repo docs as the only long-term memory of the parent migration program.
