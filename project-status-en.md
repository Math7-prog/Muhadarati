# Status Summary — University Lectures Platform

> **Project:** University Lectures Platform (Muhadarati)
> **Last updated:** October 31, 2026
> **Purpose:** a quick-start point for any team member. Summarizes everything built and agreed so far, without repeating every detail.
> **Full documents:** `project-documentation-en.md` (full specification), `open-questions-en.md` (open questions), and their matching Arabic versions.

---

## 1. General Project Status

| Item | Status |
|---|---|
| **Type** | A real product to be launched, not a prototype |
| **Team** | 4 members, may grow |
| **Deadline** | None, but ship as soon as possible |
| **Repository** | `github.com/Math7-prog/Muhadarati` (public) |
| **Project name** | **Not yet decided** — "Muhadarati" is only the repo's placeholder name |
| **Core stack** | ASP.NET Core (.NET 10 LTS) + Blazor WebAssembly |

---

## 2. Where to Find Full Documentation

| File | Content | On GitHub at |
|---|---|---|
| `project-documentation-ar.md` | Full specification in Arabic (16 sections) | `docs/ar` branch ✅ |
| `project-documentation-en.md` | Same specification in English | `docs/en` branch ✅ |
| `open-questions-ar.md` | Open questions in Arabic | `docs/ar` branch ✅ |
| `open-questions-en.md` | Same questions in English | `docs/en` branch ✅ |
| `docs/database-erd.md` | Comprehensive entity-relationship diagram for every table (Mermaid, English) | `phase-1-foundation` branch ✅ |
| `docs/data-flow-diagram.md` | Comprehensive data flow diagram for every subsystem (Mermaid, English) | `phase-1-foundation` branch ✅ |
| `project-status-ar.md` | This same summary, in Arabic | `docs/ar` branch ✅ |
| `project-status-en.md` | This same summary, in English | `docs/en` branch ✅ |

---

## 3. Technology Stack (Summary)

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core Web API — .NET 10 LTS |
| Frontend | Blazor WebAssembly |
| Database | PostgreSQL + pgvector (extension enabled; **29 tables** actually exist in the dev database — see 4.3) |
| Data access | Entity Framework Core 10 + Npgsql + `EFCore.NamingConventions` (snake_case DB naming) |
| Accounts | **ASP.NET Core Identity — the schema (tables) is fully built.** Actual sign-in logic (OTP, 2FA, JWT) **not built yet** — the next step |
| AI | `Microsoft.Extensions.AI` (unified abstraction, swappable model per feature) |
| Code editor | Monaco, with Pyodide (Python) and Judge0/Piston (C++, Java). **Confirmed: needs no additional server tables — see 4.4** |
| Student storage | Browser IndexedDB (nothing on the server) |
| Hosting (dev) | Free hosting (Render/Railway/Fly.io + Supabase/Neon) |
| Hosting (prod) | The college's server, later |
| Architecture pattern | **Clean Architecture** (Domain ← Application ← Infrastructure, Api/Web as outer layers) |
| Local dev environment | Docker for: Postgres+pgvector (host port **5433**, not 5432 — see Section 8), Piston (temporary), LibreOffice via Gotenberg, Mailpit |

---

## 4. GitHub Branch Structure

| Branch | Confirmed status |
|---|---|
| `main` | ✅ Has a professional `README.md` with two Mermaid diagrams only — no code yet (deliberate) |
| `docs/ar`, `docs/en` | ✅ Confirmed: hold both documentation files, both open-questions files, and the status summary |
| `develop`, `phase-1-foundation` … `phase-5-news-notifications`, `docs/final` | ✅ Confirmed to exist on GitHub |
| `phase-1-foundation` | ✅ Now contains: the six academic-structure entities (4.2) + Identity and all remaining tables (4.3) + the ER and data-flow diagrams (4.4) — via 3 merged pull requests |
| `feature/phase1-identity-and-core-tables` | ✅ Fully merged — no remaining work on it |

**Branching rule reminder:** `develop` is the aggregation branch; the five phase branches branch from it; small task branches (`feature/phaseN-...`) branch from their phase branch. **No direct PR to `main` except from `develop`.** **Established workflow:** every completed, verified unit of work merges into `phase-1-foundation` immediately, rather than waiting to batch steps.

### 4.1 Solution Scaffolding (October 31, 2026 — merged)
A 6-project .NET structure per Clean Architecture, `docker-compose.yml`, `Dockerfile`.

### 4.2 Core Academic Structure (October 31, 2026 — merged)
6 entities (`College`…`Lecture`) + first migration + pgvector enabled. Detailed decisions documented in `project-documentation-en.md` Section 14.1.1.

### 4.3 Completing the Database: Identity + All Remaining Tables (October 31, 2026 — merged)

Implements the team's decision to finish the whole database in one pass (schema only; registration/OTP/2FA logic later). Governing structural decisions:
1. `ApplicationUser : IdentityUser<Guid>` lives in Infrastructure only — Domain knows nothing about it.
2. Standard Identity roles (`Student`, `Supervisor`, `Admin`) for coarse classification; fine-grained detail lives in `SupervisorScopes`.
3. Extra fields on `Users`: `DisplayName`, `PreferredLanguage`, `Theme`, `UniversityEmail`, `UniversityEmailVerifiedAt`.
4. **2FA** is ready via Identity's standard mechanism; enforcing it for supervisors/admins is later application logic.
5. **JWT** instead of cookies — the package is referenced, **actual wiring into `Program.cs` has not happened yet**.

**17 new entities**, most notably `SupervisorScope` (4 scope columns + a `CHECK` constraint + **4 partial unique indexes** — a single composite index does not work correctly with nullable columns in Postgres).

**Build note:** `Muhadarati.Infrastructure.csproj` requires `<FrameworkReference Include="Microsoft.AspNetCore.App" />`, since `AddIdentity` and the core Identity types ship with the ASP.NET Core shared framework, not a standalone NuGet package.

**Actual verification:** `dotnet build` succeeded, the migration (`AddIdentityAndCoreTables`) applied, `\dt` showed exactly **29 tables**.

**Deferred:** only `LectureChunks` (waiting on Open Question 3).

### 4.4 ER Diagram and Data Flow Diagram (October 31, 2026 — merged)

Documented the full database structure and behavior in two separate Mermaid files, which render automatically on GitHub:
- **`docs/database-erd.md`** — a comprehensive entity-relationship diagram: one full diagram covering all 28 implemented tables (plus the deferred `LectureChunks`, marked as such) + 5 domain-grouped sub-diagrams (academic structure; lectures and files; accounts; news and notifications; reports and administration).
- **`docs/data-flow-diagram.md`** — a data flow diagram: a context diagram (Level 0) + an overview of the six subsystems (Level 1) + 6 detailed diagrams (registration and authentication, academic structure, lecture upload/viewing and error reports, AI requests, news and notifications, activity logging and administration).

**Location:** both files live under `docs/` at the repo root (not under `docs/ar` or `docs/en`, since their content is purely English technical diagrams, not bilingual documentation).

**Additional clarification:** the code editor needs **no additional server-side tables**:
- The personal-space storage is entirely browser IndexedDB (Sections 6.3/8.6) — outside the server database's scope to begin with.
- Execution limits (compile/run time, memory) are configured through the existing general-purpose `system_settings` table.
- Code generation is already covered by the existing 8 rows in `ai_feature_settings`.
- Judge0/Piston execution is stateless, and no run-history log is specified anywhere.

---

## 5. Final Agreed Decisions

- **Roles:** Visitor ← Student ← Supervisor (five scope levels) ← Super Admin.
- **Registration:** email + strong password, then a verification code to the university email to unlock AI. **(Schema ready; actual implementation logic not built yet.)**
- **Academic structure:** College ← Department ← Stage ← Subject ← Lecture, fully dynamic, supports shared subjects and morning/evening lectures.
- **Downloads:** always allowed on every lecture, with no way to disable it.
- **AI:** no model training — full reliance on RAG. An independent, swappable model per feature.
- **Personal space:** created by the student themselves, stored entirely on their device (IndexedDB).
- **Code editor:** Python/Java/C++/JavaScript/HTML/CSS. **Needs no additional server tables (see 4.4).**
- **Architecture:** Clean Architecture with a strict dependency rule; abstraction reserved only for what genuinely needs swapping.
- **Academic structure data model:** `Id` as Guid v7, `NameAr` required/`NameEn` optional, `Status` instead of deletion, `snake_case` naming.
- **Identity and remaining tables data model:** full detail in Section 4.3 above.
- **Database structure documentation:** a comprehensive ER diagram and a comprehensive data flow diagram, both Mermaid in English — detail in Section 4.4 above.

---

## 6. Still-Open Questions (6)

| # | Question | Owner | Priority |
|---|---|---|---|
| 1 | Choosing specific free hosting + later college server specs | Team + IT | High |
| 2 | Mail server (for verification codes and notifications) | IT | **Very high** |
| 3 | Final model adoption per feature (also fixes the deferred `LectureChunks` vector dimension) | Team | Medium |
| 4 | Judge0 or Piston | Team | Low |
| 5 | Server-side backup responsibility | IT | Medium |
| 6 | Final project name | Team | Low |

Full detail for each question in `open-questions-ar.md` / `open-questions-en.md`.

---

## 7. Next Step

**✅ Done:**
1. ~~Scaffold the .NET solution per Clean Architecture~~
2. ~~Write the initial `docker-compose.yml` and `Dockerfile`~~
3. ~~Core academic structure + first migration~~
4. ~~Identity (schema only) + all remaining tables~~
5. ~~ER diagram and data flow diagram for the whole database~~

**⏭️ Next (not started yet):**

6. **Actual registration logic:** the two-step registration API, sending/verifying the OTP, password policy, actually wiring JWT into `Program.cs`, enforcing mandatory 2FA for supervisors/admins.
7. After that: lecture upload and viewing, and the rest of Phase 1's scope.

---

## 8. Local Development Environment Notes

**Postgres port:** the project's Postgres container publishes on host port **5433** instead of the default 5432, to avoid conflicting with a native Windows PostgreSQL installation. Applied in `docker-compose.yml`, `.env`, and `.env.example`. **Do not revert this port to 5432.**

**The `.env` file:** must use **LF** line endings, not CRLF — Docker Compose parses the file the Linux way, and Windows line endings silently embed a `\r` character inside values, breaking authentication without a clear error.

**Environment variables:** never load `POSTGRES_*` variables as environment variables in the same shell session that runs `docker compose`, since Compose prefers actual environment variables over the `.env` file's values.
