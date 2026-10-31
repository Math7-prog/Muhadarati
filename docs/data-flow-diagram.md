# Muhadarati — Data Flow Diagram (DFD)

> **Purpose:** shows how data actually moves through the system — who triggers what, which process touches which database table, and which external service is involved. This is the *behavioral* companion to `database-erd.md` (which shows *structure*). Every data store named below is a real table from `database-erd.md`; nothing here invents a table that does not exist there.
> **Scope:** flows for everything implemented so far (academic structure, Identity schema, lectures metadata, news, notifications, reports, admin settings), plus the AI-feature and registration-flow data paths exactly as agreed in the specification — marked **(planned)** where the business logic itself is not built yet, only the schema that will support it.
> **Notation:** [Mermaid](https://mermaid.js.org/syntax/flowchart.html) `flowchart`. Shapes follow standard DFD convention: a rectangle `[ ]` is an external entity, a rounded/stadium shape `([ ])` is a process, and a cylinder `[( )]` is a data store (a database table). Renders automatically on GitHub — no external tool needed.
> **Companion documents:** `database-erd.md` for schema structure; `project-documentation-ar.md` / `project-documentation-en.md` for the business rules behind each flow.

---

## 1. Context Diagram (Level 0)

The whole platform treated as a single process, showing only who/what it exchanges data with.

```mermaid
flowchart TD
    VIS[Visitor]
    STU[Student]
    SUP[Supervisor]
    ADM[Super Admin]
    AIP[AI Providers]
    MAIL[Mail Server]
    EXEC[Code Execution Service]
    CONV[LibreOffice Conversion Service]

    VIS -->|browses structure and news| SYS(((Muhadarati Platform)))
    SYS -->|public structure and news| VIS

    STU -->|registers, uploads, chats, downloads| SYS
    SYS -->|lectures, AI answers, notifications| STU

    SUP -->|manages scoped content| SYS
    SYS -->|scoped data and reports| SUP

    ADM -->|manages structure, users, settings| SYS
    SYS -->|full platform data| ADM

    SYS -->|prompts and lecture text| AIP
    AIP -->|AI generated answers| SYS

    SYS -->|verification codes and notifications| MAIL

    SYS -->|source code and input| EXEC
    EXEC -->|execution output| SYS

    SYS -->|office files| CONV
    CONV -->|converted PDF| SYS
```

---

## 2. System Overview (Level 1)

The single process above, broken into its six major subsystems, each paired with the group of tables it owns.

```mermaid
flowchart TD
    STU[Student]
    SUP[Supervisor]
    ADM[Super Admin]

    P1([Identity and Access])
    P2([Academic Structure Management])
    P3([Lecture Management])
    P4([AI Services])
    P5([News and Notifications])
    P6([Reports and Administration])

    DS1[(Identity Tables)]
    DS2[(Academic Structure Tables)]
    DS3[(Lecture Tables)]
    DS4[(AI Settings and Usage)]
    DS5[(News and Notification Tables)]
    DS6[(Reports and Logs)]

    STU --> P1
    SUP --> P1
    ADM --> P1
    P1 <--> DS1

    ADM --> P2
    SUP --> P2
    P2 <--> DS2

    SUP --> P3
    STU --> P3
    P3 <--> DS3
    P3 --> P2

    STU --> P4
    P4 <--> DS4
    P4 --> P3

    SUP --> P5
    ADM --> P5
    STU --> P5
    P5 <--> DS5

    STU --> P6
    SUP --> P6
    ADM --> P6
    P6 <--> DS6
    P1 --> P6
    P2 --> P6
    P3 --> P6
```

---

## 3. Registration and Authentication *(planned business logic, schema already built)*

```mermaid
flowchart TD
    VIS[Visitor]
    MAIL[Mail Server]

    P1([Register with Email and Password])
    P2([Send University Email OTP])
    P3([Verify OTP])
    P4([Set Primary Stage])
    P5([Authenticate and Issue JWT])

    USERS[(users)]
    STAGES[(stages)]
    SP[(student_profiles)]
    ROLES[(roles and user_roles)]

    VIS --> P1
    P1 -->|creates account| USERS

    USERS --> P2
    P2 -->|sends code| MAIL

    VIS --> P3
    P3 -->|reads pending code, not stored in the database| P2
    P3 -->|sets university_email_verified_at| USERS

    VIS --> P4
    STAGES -->|list of stages| P4
    P4 -->|writes primary_stage_id| SP

    VIS --> P5
    USERS -->|credentials check| P5
    ROLES -->|role claims| P5
    P5 -->|JWT bearer token, not stored| VIS
```

**Note:** the OTP code itself is intentionally never written to a persistent table (Section 3.1 of the specification) — it lives in a short-lived cache, which is why the diagram shows no data store for it.

---

## 4. Academic Structure Management

```mermaid
flowchart TD
    ADM[Super Admin]
    SUP[Supervisor]

    P1([Manage Colleges and Departments])
    P2([Manage Stages])
    P3([Manage Subjects and Links])
    P4([Assign Supervisor Scope])

    COL[(colleges)]
    DEP[(departments)]
    STG[(stages)]
    SUB[(subjects)]
    SSL[(subject_stage_links)]
    SCOPE[(supervisor_scopes)]

    ADM --> P1
    P1 <--> COL
    P1 <--> DEP

    ADM --> P2
    SUP --> P2
    DEP -->|parent department| P2
    P2 <--> STG

    ADM --> P3
    SUP --> P3
    STG -->|owning stage| P3
    P3 <--> SUB
    P3 <--> SSL

    ADM --> P4
    USERS[(users)] -->|target user| P4
    P4 <--> SCOPE
```

---

## 5. Lecture Upload, Viewing and Error Reporting

```mermaid
flowchart TD
    SUP[Supervisor]
    STU[Student]
    CONV[LibreOffice Conversion Service]

    P1([Check Upload Permission])
    P2([Upload Lecture])
    P3([Convert File to PDF])
    P4([View or Download Lecture])
    P5([Report an Error])

    SCOPE[(supervisor_scopes)]
    SUB[(subjects)]
    LEC[(lectures)]
    LF[(lecture_files)]
    LVL[(lecture_video_links)]
    LVS[(lecture_view_stats)]
    ER[(error_reports)]

    SUP --> P1
    SCOPE -->|can_upload check| P1
    P1 --> P2

    P2 -->|needs subject| SUB
    P2 -->|writes| LEC
    P2 -->|writes| LF
    P2 -->|writes| LVL

    LF --> P3
    P3 --> CONV
    CONV -->|converted file| P3
    P3 -->|updates converted_file_path| LF

    STU --> P4
    LEC -->|metadata| P4
    LF -->|files| P4
    P4 -->|increments counters| LVS

    STU --> P5
    LEC -->|lecture reference| P5
    LF -->|optional file reference| P5
    P5 -->|writes| ER
```

---

## 6. AI Feature Request *(RAG chat path shown; other features follow the same shape)*

```mermaid
flowchart TD
    STU[Student]
    AIP[AI Providers]

    P1([Resolve Provider and Model for Feature])
    P2([Retrieve Relevant Chunks - deferred])
    P3([Send Prompt and Record Usage])

    AFS[(ai_feature_settings)]
    LC[(lecture_chunks - deferred)]
    AUS[(ai_usage_stats)]

    STU -->|asks a question about a lecture| P1
    AFS -->|provider and model for Chat| P1
    P1 --> P2

    LC -->|closest text chunks - Phase 2| P2
    P2 --> P3

    P3 -->|prompt plus retrieved context| AIP
    AIP -->|generated answer| STU
    P3 -->|increments daily count| AUS
```

---

## 7. News Publishing and Notifications

```mermaid
flowchart TD
    SUP[Supervisor]
    ADM[Super Admin]
    STU[Student]
    MAIL[Mail Server]

    P1([Check Publish Permission])
    P2([Publish News])
    P3([Generate Notification])
    P4([Set Notification Preferences])
    P5([Dispatch Email])

    SCOPE[(supervisor_scopes)]
    NEWS[(news)]
    NT[(news_targets)]
    NOTIF[(notifications)]
    NP[(notification_preferences)]
    LEC[(lectures)]

    SUP --> P1
    ADM --> P1
    SCOPE -->|can_publish_news check| P1
    P1 --> P2
    P2 -->|writes| NEWS
    P2 -->|writes| NT

    NEWS --> P3
    LEC -->|new lecture event| P3
    P3 -->|writes| NOTIF

    STU --> P4
    P4 -->|writes| NP

    NOTIF --> P5
    NP -->|email preference check| P5
    P5 --> MAIL
```

---

## 8. Activity Logging and System Administration *(cross-cutting)*

```mermaid
flowchart TD
    SUP[Supervisor]
    ADM[Super Admin]

    P1([Any Create, Update, Delete, or Publish Action])
    P2([Log Activity])
    P3([Manage System Settings])
    P4([Manage AI Feature Settings])

    AL[(activity_logs)]
    SS[(system_settings)]
    AFS[(ai_feature_settings)]

    SUP --> P1
    ADM --> P1
    P1 --> P2
    P2 -->|writes action, entity_type, entity_id| AL

    ADM --> P3
    P3 <--> SS

    ADM --> P4
    P4 <--> AFS
```

**Note:** Step P1 to P2 represents every write operation shown in Sections 3 to 7 above — uploading a lecture, publishing news, editing the academic structure, and so on. It is drawn once here rather than repeated on every earlier diagram, to keep each of them readable.

---

## Change log

| Date | Change |
|---|---|
| October 31, 2026 | Initial version — eight diagrams covering context, system overview, and all six major subsystems, grounded in the 28 implemented tables from `database-erd.md` |
