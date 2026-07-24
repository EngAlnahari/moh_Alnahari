
# HHH System Reverse Engineering Master Document

## 1. Executive Summary

### 1.1 Purpose of This Document

This document records the reverse engineering analysis of the existing HHH system based only on the available source files and generated analysis files.

The purpose is to document the actual existing structure, pages, visible components, forms, controls, dependencies, and observed behaviour.

This document does not describe future development plans and does not assume any functionality that is not visible in the source materials.

### 1.2 Analysis Source

The analysis is based on:

- Existing HTML files located inside:

```
D:\EngMohammed\hhh
```

- Existing analysis files generated from the source system.

The documented information is limited to what can be observed from these sources.

### 1.3 Analysis Method

The analysis method used is:

- File inventory review.
- HTML structure review.
- Visible UI element identification.
- Form and input field identification.
- Button and interaction identification.
- Dependency identification.

Any functionality not directly visible in the available files is not documented as an existing feature.

---

## 2. Source System Inventory

### 2.1 Main Source Directory

The analyzed source directory is:

```
D:\EngMohammed\hhh
```

### 2.2 Available HTML Files

The following HTML files were identified:

| File | Size |
|---|---:|
| AllTemplate.html | 92082 bytes |
| almjal.html | 45789 bytes |
| almjal1.html | 37109 bytes |
| ContractTemplate.html | 21251 bytes |
| ContractTemplate1.html | 17462 bytes |
| create.html | 60820 bytes |
| createdevided.html | 49067 bytes |
| createform.html | 53544 bytes |
| createorder.html | 39902 bytes |
| createspece.html | 10944 bytes |
| creatform.html | 53767 bytes |
| devid.html | 12864 bytes |
| index.html | 27604 bytes |
| WorkOffer.html | 13128 bytes |
| WorkOffer1.html | 3699 bytes |
| Work_Form.html | 26654 bytes |
| Work_Form_base.html | 33328 bytes |

### 2.3 Supporting Files

The following supporting files were identified:

| File | Purpose Observed |
|---|---|
| localizer.py | Python file موجود ضمن المجلد |
| libs/bootstrap.bundle.min.js | JavaScript library |
| libs/bootstrap.rtl.min.css | CSS library |
| libs/jquery.min.js | JavaScript library |

---

```


## 3. System Architecture

### 3.1 Observed Structure

Based on the available source files, the HHH system appears as a collection of HTML pages with supporting JavaScript and CSS resources.

The available structure contains:

- HTML pages.
- JavaScript code embedded inside HTML files.
- External JavaScript libraries.
- External CSS libraries.
- A Python utility file.

No backend source code, database files, API files, or server-side implementation files were identified in the available inventory.

### 3.2 HTML Page Structure

The identified pages contain different user interfaces and forms.

Observed page categories include:

- Main/template pages.
- Request creation pages.
- Work form pages.
- Contract pages.
- Offer pages.
- Domain/category selection pages.

The classification above is based only on visible page titles and HTML contents.

### 3.3 Front-End Dependencies

The following front-end dependencies were identified:

#### Bootstrap

File:

```
libs/bootstrap.bundle.min.js
```

Observed role:

- JavaScript library file included in the system resources.

File:

```
libs/bootstrap.rtl.min.css
```

Observed role:

- RTL CSS styling resource.

#### jQuery

File:

```
libs/jquery.min.js
```

Observed role:

- JavaScript library resource.

### 3.4 Client-Side Implementation

The available files show client-side behaviours implemented through:

- HTML elements.
- JavaScript functions.
- Button events.
- Dynamic form operations.
- Table row operations.
- Form interaction logic.

The exact internal logic is documented later in the JavaScript Behaviour section based on the available source analysis.

### 3.5 Architecture Limitations

Based on the available files:

Confirmed:

- The system contains HTML-based interfaces.
- The system uses JavaScript and CSS resources.
- The system contains multiple functional pages.

Not confirmed from available files:

- Database architecture.
- Server-side architecture.
- Authentication system.
- User permission system.
- API architecture.
- Deployment architecture.
```

---

- User permission system.
- API architecture.
- Deployment architecture.
```

ضع المؤشر بعده، اضغط Enter مرتين.

ثم انسخ النص التالي والصقه:

---

```markdown
## 4. Pages and Modules

This section documents the pages identified from the available HHH source files.

The descriptions are based only on visible file names, page titles, and HTML elements found in the source analysis.

---

# 4.1 AllTemplate.html

## File Information

File:

```
AllTemplate.html
```

Size:

```
92082 bytes
```

## Observed Page Title

The page contains:

```
السيرة الذاتية
```

## Observed Forms and Sections

The page contains multiple HTML sections including:

- Request creation form.
- Work form interface.
- Offers interface.

## Observed Request Form Elements

The following fields and controls were identified:

### Personal Description

Field:

```
tanjezOrder.DescripPerson
```

Type:

Select element.

---

### Location Information

Observed fields:

```
tanjezOrder.Zone
```

Description:

المديرية


```
tanjezOrder.Directorate
```

Description:

العزلة


```
tanjezOrder.Village
```

Description:

القرية او الحي

---

### Coordinates

Observed fields:

```
tanjezOrder.CoordinatesX
```

and

```
tanjezOrder.CoordinatesY
```

Type:

Numeric input fields.

---

### Place Information

Observed fields:

```
tanjezOrder.PlaceName
```

Description:

Request title / place name.

```
tanjezOrder.PlaceType
```

Description:

Place type selection.

---

### Area Information

Observed fields:

```
tanjezOrder.Space
```

Type:

Numeric input.

---

### Unit Information

Observed elements:

```
tanjezOrder.Unit
```

and:

```
tanjezOrder.AcountPiese
```

Related to unit selection and count.

---

### Land Borders Section

Observed section:

```
earthBordersSection
```

Contains:

- Border rows.
- Border type selection.
- Border description fields.
- Difference fields.
- Remove border buttons.
- Add border row button.

Observed button:

```
addBorderRow
```

---

### Building Section

Observed section:

```
buildingTableSection
```

Contains:

- Building table.
- Add building row button.

Observed button:

```
addBuildingRow
```

---

### Attachments

Observed elements:

- File attachment inputs.
- Add attachment button.
- Remove attachment buttons.

Observed identifiers:

```
addAttachmentBtn
```

---

### Names Sections

Observed controls:

```
addNameBtn1
```

and

```
addNameBtn2
```

Used with name list fields.

---

# 4.2 almjal.html

## File Information

File:

```
almjal.html
```

Size:

```
45789 bytes
```

## Observed Page Title

```
نظام شجرة المجالات
```

## Observed Interface Flow

The page contains a selection flow:

1. Choose main domain.
2. Choose specialty.
3. Choose branch.
4. Choose detail.

## Observed Buttons

The page contains navigation buttons including:

```
back-to-main
```

```
back-to-specialty
```

```
back-to-branch
```

Functional buttons:

```
btn-requests
```

```
btn-work-form
```

```
btn-work-contract
```

```
btn-work-wofer
```

---

# 4.3 almjal1.html

## File Information

File:

```
almjal1.html
```

Size:

```
37109 bytes
```

## Observed Page Title

```
شجرة المجالات الهندسية والقانونية والقضاء
```

The file contains interface elements related to domain tree display.

---

```

---

The file contains interface elements related to domain tree display.
```

# 4.4 ContractTemplate.html

## File Information

File:

```
ContractTemplate.html
```

Size:

```
21251 bytes
```

## Observed Page Title

```
لوحة العقود للمهندس
```

## Observed Interface Elements

The page contains contract-related interface elements including:

### Contract Approval

Observed button:

```
approveBtn
```

Text:

```
الموافقة على العقد
```

---

### Contract Rejection

Observed controls:

- اعتراض على أحد البنود.
- إضافة أو تعديل بند.
- رفض العقد.

Observed button:

```
rejectBtn
```

The page contains a rejection form:

```
rejectForm
```

---

### Payment Interface

Observed payment section containing:

Buttons:

```
credit_card
```

and

```
sent_to_client
```

Observed payment form:

```
paymentForm
```

Observed actions:

- Select payment method.
- Confirm payment.
- Cancel payment.

---

# 4.5 ContractTemplate1.html

## File Information

File:

```
ContractTemplate1.html
```

Size:

```
17462 bytes
```

## Observed Page Title

```
لوحة العقود للمهندس
```

## Observed Similar Elements

The file contains similar contract interface elements:

- Contract approval button.
- Contract rejection controls.
- Rejection form.
- Payment selection interface.
- Payment confirmation form.

Observed identifiers:

```
approveBtn
```

```
rejectBtn
```

```
rejectForm
```

```
paymentForm
```

---

# 4.6 create.html

## File Information

File:

```
create.html
```

Size:

```
60820 bytes
```

## Observed Page Title

```
السيرة الذاتية
```

## Observed Main Elements

The page contains a request creation interface.

Observed elements include:

- Main request form.
- Location fields.
- Coordinates fields.
- Land borders section.
- Building section.
- Attachments section.

Observed controls include:

```
attachBtn
```

```
addBorderRow
```

```
addBuildingRow
```

```
addAttachmentBtn
```

```
saveRequestBtn
```

---

# 4.7 createdevided.html

## File Information

File:

```
createdevided.html
```

Size:

```
49067 bytes
```

## Observed Elements

The page contains:

- Request creation form.
- Border management controls.
- Building table controls.
- Attachment controls.

Observed buttons:

```
addBorderRow
```

```
addBuildingRow
```

```
addAttachmentBtn
```

```
saveRequestBtn
```

---

# 4.8 createform.html

## File Information

File:

```
createform.html
```

Size:

```
53544 bytes
```

## Observed Elements

The page contains:

- Request form interface.
- Border section.
- Building section.
- Attachment section.
- Name input sections.

Observed controls:

```
addBorderRow
```

```
addBuildingRow
```

```
addAttachmentBtn
```

```
addNameBtn1
```

```
addNameBtn2
```

```
saveRequestBtn
```

---

```

---

---


# 4.9 createorder.html

## File Information

File:

```
createorder.html
```

Size:

```
39902 bytes
```

## Observed Elements

The page contains a request creation interface.

Observed sections include:

- Request form.
- Border information section.
- Building information section.
- Attachment section.

Observed controls:

```
attachBtn
```

Attachment button.

```
addBorderRow
```

Add border row control.

```
addBuildingRow
```

Add building row control.

```
addAttachmentBtn
```

Add attachment control.

```
saveRequestBtn
```

Save request button.

---

# 4.10 createspece.html

## File Information

File:

```
createspece.html
```

Size:

```
10944 bytes
```

## Observed Page Title

```
السيرة الذاتية
```

## Observed Elements

The page contains:

- Request form.
- Attachment section.

Observed controls:

```
addAttachmentBtn
```

Attachment addition control.

```
saveRequestBtn
```

Save request button.

---

# 4.11 creatform.html

## File Information

File:

```
creatform.html
```

Size:

```
53767 bytes
```

## Observed Elements

The page contains:

- Request form interface.
- Border section.
- Building section.
- Attachment section.
- Name sections.

Observed controls:

```
addBorderRow
```

```
addBuildingRow
```

```
addAttachmentBtn
```

```
addNameBtn1
```

```
addNameBtn2
```

---

# 4.12 devid.html

## File Information

File:

```
devid.html
```

Size:

```
12864 bytes
```

## Observed Elements

The file exists within the source directory.

Detailed interface elements require further inspection from the source file.

---

# 4.13 WorkOffer.html

## File Information

File:

```
WorkOffer.html
```

Size:

```
13128 bytes
```

## Observed Page Content

The file contains an interface related to work offers.

Observed title/content references:

```
عرض جديد
```

## Observed Elements

The page contains:

- Offer interface.
- Form elements.
- Save-related controls.

---

# 4.14 WorkOffer1.html

## File Information

File:

```
WorkOffer1.html
```

Size:

```
3699 bytes
```

## Observed Elements

The file exists within the source directory.

Detailed elements require further source inspection.

---

# 4.15 Work_Form.html

## File Information

File:

```
Work_Form.html
```

Size:

```
26654 bytes
```

## Observed Elements

The page contains a work form interface.

Observed elements include:

- Work form fields.
- Dynamic table rows.
- Save form button.
- Report sending action.

Observed buttons:

```
addTableRow()
```

Add table row action.

```
saveData(event)
```

Save work form action.

```
sendReportToClient()
```

Send report action.

---

# 4.16 Work_Form_base.html

## File Information

File:

```
Work_Form_base.html
```

Size:

```
33328 bytes
```

## Observed Elements

The file exists as part of the source system.

It is related to the work form pages based on its filename.

Detailed contents require direct file inspection.

---

# 4.17 index.html

## File Information

File:

```
index.html
```

Size:

```
27604 bytes
```

## Observed Elements

The file exists as a main HTML page within the source directory.

Detailed interface elements require direct source inspection.

---

```

---

# 5. Functional Map

This section maps the visible functional areas identified from the available HHH source files.

The map is based on existing pages, forms, controls, and interface elements.

It does not describe hidden business logic that is not available in the source files.

---

# 5.1 Request Creation Area

## Related Files

Observed files:

- AllTemplate.html
- create.html
- createdevided.html
- createform.html
- createorder.html
- createspece.html
- creatform.html

## Observed Components

The request creation pages contain visible elements including:

- Form interfaces.
- Location information.
- Coordinate inputs.
- Place information.
- Area information.
- Land borders.
- Building information.
- Attachments.
- Name entry sections.

---

# 5.2 Domain Selection Area

## Related Files

```
almjal.html
```

and

```
almjal1.html
```

## Observed Components

The pages contain domain selection interfaces.

Observed navigation sequence:

1. Select domain.
2. Select specialty.
3. Select branch.
4. Select detail.

Observed navigation controls:

- Back navigation buttons.
- Request button.
- Work form button.
- Contract button.
- Offer button.

---

# 5.3 Contract Area

## Related Files

```
ContractTemplate.html
```

```
ContractTemplate1.html
```

## Observed Components

The contract pages contain:

- Contract display interface.
- Approval control.
- Rejection controls.
- Modification request controls.
- Payment selection interface.

Observed actions:

- Approve contract.
- Reject contract.
- Add or modify clause.
- Select payment method.
- Confirm payment.

---

# 5.4 Work Form Area

## Related Files

```
Work_Form.html
```

```
Work_Form_base.html
```

## Observed Components

The work form pages contain:

- Work form interface.
- Dynamic tables.
- Row addition controls.
- Save action.
- Report sending action.

Observed JavaScript actions:

```
addTableRow()
```

```
saveData(event)
```

```
sendReportToClient()
```

---

# 5.5 Work Offer Area

## Related Files

```
WorkOffer.html
```

```
WorkOffer1.html
```

## Observed Components

The files contain interfaces related to work offers.

Observed elements:

- Offer page interface.
- Offer form elements.
- Save-related controls.

---

# 5.6 Shared Interface Components

Across multiple pages the following repeated components were observed:

## Forms

Multiple HTML forms are used for data entry.

Examples:

```
<form>
```

and forms with identifiers such as:

```
tanjezForm
```

---

## Tables

Observed table usage includes:

- Border tables.
- Building tables.
- Work form tables.

---

## Dynamic Row Controls

Observed controls:

```
addBorderRow
```

```
addBuildingRow
```

```
addTableRow()
```

Used for adding rows dynamically.

---

## Attachment Controls

Observed controls:

```
addAttachmentBtn
```

```
remove-attachment
```

Used for managing attachment inputs.

---

# 5.7 Functional Relationship Summary

Based on visible source files, the system contains the following connected interface areas:

```
Domain Selection
        |
        |
Request Forms
        |
        |
Work Forms
        |
        |
Contracts
        |
        |
Offers
```

This represents the visible page relationship only.

The actual data flow between these areas cannot be confirmed without backend source code or database structure.

```

---

# 7. Business Rules

## 7.1 Overview

This section documents the business rules that can be identified from the existing HHH HTML system source files.

The rules documented here are based only on observable evidence from:
- HTML structure
- Form fields
- Input names
- Element identifiers
- Buttons
- JavaScript interactions
- Existing page organization

No business rule is assumed unless there is direct evidence in the source files.

---

## 7.2 Request Creation Rules

The system contains multiple pages related to creating requests and work records.

Observed evidence:

- Multiple HTML pages contain forms using identifiers related to request creation.
- Main forms use fields connected to request information.
- The main request form appears to collect information about the requested service, location, property information, attachments, and related details.

Observed fields include:

- Request description
- Location information
- Coordinates
- Place name
- Place type
- Area information
- Unit information
- Attachments
- Notes


---

## 7.3 Location Data Rules

The system collects geographic information related to the request.

Observed fields:

- Zone
- Directorate
- Village or neighbourhood
- Coordinates X
- Coordinates Y
- Place name


Evidence examples:

```text
tanjezOrder.Zone
tanjezOrder.Directorate
tanjezOrder.Village
tanjezOrder.CoordinatesX
tanjezOrder.CoordinatesY
tanjezOrder.PlaceName
```


Interpretation:

The system requires location-related information as part of the request creation workflow.

No further validation rules were identified from the available analysis files.

---

## 7.4 Property and Area Rules

The system contains fields related to land/property information.

Observed elements:

- Property type selection
- Area input
- Unit selection
- Number of pieces
- Area difference option


Observed fields:

```text
tanjezOrder.PlaceType
tanjezOrder.Space
tanjezOrder.Unit
tanjezOrder.AcountPiese
tanjezOrder.AreaDifferent
```


Interpretation:

The system supports collecting different types of property measurements.

The exact calculation logic is not documented unless found in JavaScript source analysis.

---

## 7.5 Boundary Information Rules

The system contains a dedicated section for land boundaries.

Observed features:

- Four boundary directions.
- Boundary description fields.
- Boundary type selections.
- Difference/variation fields.
- Ability to remove or add boundary rows.


Observed identifiers:

```text
earthBorders
BorderType
BorderDescription
Difference
IdenticalOrDifferent
```


Interpretation:

The system models property boundaries as structured records rather than a single text field.

---

## 7.6 Attachment Rules

The system supports adding files or images.

Observed elements:

- Attachment button.
- File input controls.
- Multiple attachment rows.
- Remove attachment buttons.


Observed identifiers:

```text
attachBtn
docImageInput
fileAttach[]
addAttachmentBtn
remove-attachment
```


Interpretation:

Users can attach supporting documents to requests.

The exact storage mechanism is not visible in the HTML source.

---

## 7.7 Workflow Related Rules

The system contains several workflow-related pages:

Observed pages:

- Request creation pages.
- Work form pages.
- Contract pages.
- Offer pages.


Observed actions:

- Save request.
- Save work form.
- Send report to client.
- Approve contract.
- Reject contract.
- Payment selection.


Interpretation:

The system represents a workflow moving from request creation toward service execution and contractual/payment stages.

The exact workflow sequence requires deeper JavaScript and backend analysis.

---

## 7.8 Contract Related Rules

Observed contract-related features:

- Contract approval.
- Contract rejection.
- Adding or modifying clauses.
- Payment method selection.


Observed actions:

```text
approveBtn
rejectBtn
showRejectForm()
showPaymentForm()
submitPayment()
```


Interpretation:

The contract module supports user interaction around approval, rejection, and payment confirmation.

---

## 7.9 Limitations

The following rules cannot be confirmed from the current evidence:

- User permission rules.
- Database constraints.
- Mandatory fields enforced by backend.
- Pricing calculations.
- Payment processing logic.
- Approval hierarchy.
- Notification mechanisms.

These require backend source code or additional system documentation.
```

# 8. Data Model Candidates

## 8.1 Overview

This section identifies possible data entities and relationships that can be inferred from the existing HHH HTML system.

Important:

The following model is not a confirmed database schema.

It represents only candidate data structures derived from:

- HTML form fields
- Input names
- Select elements
- Identifiers
- Repeated structures
- Page workflows

No database tables, columns, or relationships are assumed unless supported by source evidence.

---

# 8.2 Main Request Entity Candidate

## Entity: Request / tanjezOrder

The system contains multiple fields using the prefix:

```text
tanjezOrder
```

This indicates the existence of a central object or model representing a request.

Observed fields:

```text
tanjezOrder.DescripPerson
tanjezOrder.Zone
tanjezOrder.Directorate
tanjezOrder.Village
tanjezOrder.CoordinatesX
tanjezOrder.CoordinatesY
tanjezOrder.PlaceName
tanjezOrder.PlaceType
tanjezOrder.Space
tanjezOrder.Unit
tanjezOrder.AcountPiese
tanjezOrder.AreaDifferent
tanjezOrder.DocumentImage
tanjezOrder.PeriodType
tanjezOrder.MaximumDuration
tanjezOrder.Transportation
tanjezOrder.OverNight
tanjezOrder.Allowance
```

Possible purpose:

Stores the main information submitted by the user when creating a request.

---

# 8.3 Location Entity Candidate

Observed location-related fields:

```text
Zone
Directorate
Village
CoordinatesX
CoordinatesY
PlaceName
```

Possible entity:

```text
Location
```

Possible attributes:

```text
Zone
Directorate
Village
CoordinatesX
CoordinatesY
PlaceName
```

Evidence:

The fields are collected together inside request creation forms.

---

# 8.4 Property Entity Candidate

Observed property-related information:

```text
PlaceType
Space
Unit
AcountPiese
AreaDifferent
```

Possible entity:

```text
Property
```

Possible attributes:

```text
PropertyType
Area
MeasurementUnit
PieceCount
AreaDifferenceFlag
```

Evidence:

These fields appear in sections related to land/property information.

---

# 8.5 Boundary Entity Candidate

The system contains repeated boundary structures.

Observed fields:

```text
earthBorders[0].BorderType
earthBorders[0].BorderDescription
earthBorders[0].Difference

earthBorders[1].BorderType
earthBorders[1].BorderDescription
earthBorders[1].Difference

earthBorders[2].BorderType
earthBorders[2].BorderDescription
earthBorders[2].Difference

earthBorders[3].BorderType
earthBorders[3].BorderDescription
earthBorders[3].Difference
```

Possible entity:

```text
EarthBoundary
```

Possible attributes:

```text
BorderType
BorderDescription
Difference
IdenticalOrDifferent
```

Possible relationship:

```text
Request
   |
   |---- has many ---- EarthBoundary
```

Evidence:

The HTML structure supports multiple boundary records.

---

# 8.6 Building Entity Candidate

The system contains a building section.

Observed elements:

```text
buildingTableSection
addBuildingRow
```

Possible entity:

```text
Building
```

Possible attributes:

Not fully identifiable from current analysis.

Further JavaScript analysis is required.

---

# 8.7 Attachment Entity Candidate

Observed attachment controls:

```text
docImageInput
fileAttach[]
addAttachmentBtn
remove-attachment
```

Possible entity:

```text
Attachment
```

Possible attributes:

```text
File
FileType
RelatedRequest
```

Evidence:

The interface allows multiple files to be associated with a request.

---

# 8.8 Work Form Entity Candidate

Observed page:

```text
Work_Form.html
Work_Form_base.html
```

Observed actions:

```text
saveData(event)
sendReportToClient()
```

Possible entity:

```text
WorkForm
```

Possible relationship:

```text
Request
   |
   |---- creates ---- WorkForm
```

The exact fields require further source analysis.

---

# 8.9 Contract Entity Candidate

Observed pages:

```text
ContractTemplate.html
ContractTemplate1.html
```

Observed actions:

```text
approveBtn
rejectBtn
paymentForm
submitPayment()
```

Possible entity:

```text
Contract
```

Possible attributes:

```text
ApprovalStatus
RejectionReason
PaymentMethod
```

These are inferred from visible interface actions only.

---

# 8.10 Offer Entity Candidate

Observed page:

```text
WorkOffer.html
WorkOffer1.html
```

Observed feature:

```text
عرض جديد
```

Possible entity:

```text
WorkOffer
```

Further analysis is required to identify its complete attributes.

---

# 8.11 Candidate Relationship Diagram

Based on observed evidence:

```text
Request
 |
 |---- Location
 |
 |---- Property
 |
 |---- EarthBoundary[]
 |
 |---- Building[]
 |
 |---- Attachment[]
 |
 |---- WorkForm
 |
 |---- Contract
 |
 |---- WorkOffer
```

This diagram represents a possible logical model only.

It is not a confirmed database design.

---

# 8.12 Missing Data Model Information

The following cannot be determined from the current HTML analysis:

- Primary keys
- Foreign keys
- Database tables
- Entity constraints
- Data types
- Validation rules
- Stored procedures
- Backend relationships

Additional backend source code would be required.
```

---


## 9.1 Overview

This section documents the user journeys that can be identified from the existing HHH HTML system.

The journeys are reconstructed only from visible evidence in:

- Pages
- Forms
- Buttons
- Navigation elements
- User actions
- Interface sections

No user role, permission, or workflow step is assumed unless supported by the available source evidence.

---

# 9.2 Main Request Creation Journey

## Journey Name

Create New Request

## Source Evidence

Observed pages:

```text
create.html
createform.html
createorder.html
AllTemplate.html
createdevided.html
```

Observed form:

```text
tanjezForm
```

---

## User Flow

```text
Open Request Creation Page

        |
        v

Enter Request Information

        |
        v

Enter Location Information

        |
        v

Enter Property Information

        |
        v

Add Boundaries

        |
        v

Add Attachments

        |
        v

Save Request
```

---

## Step 1: Enter Request Information

Observed fields include:

```text
DescripPerson
PlaceName
PlaceType
Notes
```

Purpose:

Collect basic information describing the requested service.

---

## Step 2: Enter Location Information

Observed fields:

```text
Zone
Directorate
Village
CoordinatesX
CoordinatesY
PlaceName
```

Purpose:

Associate the request with a geographic location.

---

## Step 3: Enter Property Information

Observed fields:

```text
PlaceType
Space
Unit
AcountPiese
AreaDifferent
```

Purpose:

Capture property or land measurement information.

---

## Step 4: Define Boundaries

Observed interface:

```text
earthBordersSection
```

Available actions:

```text
Add Border Row
Remove Border Row
```

Purpose:

Record boundary descriptions around the property.

---

## Step 5: Add Attachments

Observed controls:

```text
attachBtn
docImageInput
addAttachmentBtn
remove-attachment
```

Purpose:

Allow users to provide supporting documents or images.

---

## Step 6: Save Request

Observed button:

```text
saveRequestBtn
```

Purpose:

Submit the completed request form.

---

# 9.3 Work Form Journey

## Journey Name

Complete Work Form

## Source Evidence

Observed pages:

```text
Work_Form.html
Work_Form_base.html
```

Observed actions:

```text
saveData(event)

sendReportToClient()
```

---

## User Flow

```text
Open Work Form

        |
        v

Enter Work Details

        |
        v

Save Work Form

        |
        v

Send Report To Client
```

---

## Notes

The exact work fields and validation rules are not available in the current HTML analysis.

Further JavaScript analysis is required.

---

# 9.4 Contract Journey

## Journey Name

Review and Process Contract

## Source Evidence

Observed pages:

```text
ContractTemplate.html
ContractTemplate1.html
```

---

## User Flow

```text
Open Contract

        |
        v

Review Contract Content

        |
        v

Approve
or
Reject

        |
        v

Select Payment Method
```

---

## Observed Actions

Approval:

```text
approveBtn
```

Rejection:

```text
rejectBtn
```

Payment:

```text
showPaymentForm()
submitPayment()
```

---

## Notes

The system interface supports contract decisions.

The following are not confirmed:

- Who can approve.
- Who can reject.
- Contract lifecycle rules.
- Payment backend integration.

---

# 9.5 Offer Journey

## Journey Name

Create or Review Work Offer

## Source Evidence

Observed pages:

```text
WorkOffer.html
WorkOffer1.html
```

---

## Observed Features

```text
عرض جديد
```

Possible user actions:

- Create offer.
- Review offer.
- Submit offer.

The exact workflow cannot be confirmed from current evidence.

---

# 9.6 Domain Selection Journey

## Journey Name

Select Engineering / Legal Domain

## Source Evidence

Observed pages:

```text
almjal.html
almjal1.html
```

---

## User Flow

```text
Open Domain Tree

        |
        v

Select Domain

        |
        v

Select Specialty

        |
        v

Select Branch

        |
        v

Choose Action
```

---

## Observed Actions

```text
الطلبات

استمارة العمل

عقد العمل

العروض
```

---

# 9.7 Overall User Journey Map

Based on current evidence:

```text
Domain Selection

        |
        v

Create Request

        |
        v

Work Execution

        |
        v

Contract Processing

        |
        v

Payment / Completion
```

This represents an inferred journey map based on interface structure.

It is not a confirmed operational workflow.

---

# 9.8 Missing Journey Information

The following cannot be determined:

- User accounts
- Login process
- User permissions
- Notifications
- Approval hierarchy
- Backend state transitions
- Data ownership

Additional backend analysis is required.

# 10. Workflows

## 10.1 Workflow Analysis Overview

This section documents workflows identified from the existing HHH HTML files.

The workflow descriptions are based only on:

- Page structure.
- Forms.
- Buttons.
- HTML controls.
- Visible JavaScript function references.

Backend processing and database workflows are not available in the analyzed files.

---

# 10.2 Request Form Workflow

## Source Files

```text
create.html
createform.html
createorder.html
createdevided.html
AllTemplate.html
```

---

## Observed Workflow Elements

Main form:

```text
tanjezForm
```

Observed controls:

```text
Input fields
Select fields
Tables
Attachment controls
Save button
```

---

## Form Data Sections

Observed sections include:

```text
Request Information

Location Information

Property Information

Earth Borders

Buildings

Attachments

Notes
```

---

## Dynamic Actions

Observed buttons:

```text
addBorderRow

remove-border

addBuildingRow

addAttachmentBtn

remove-attachment

addNameBtn1

addNameBtn2
```

---

## Submit Action

Observed button:

```text
saveRequestBtn
```

The HTML shows a submit action.

The processing logic after submission is not available in the analyzed HTML files.

---

# 10.3 Work Form Workflow

## Source Files

```text
Work_Form.html
Work_Form_base.html
```

---

## Observed Elements

Form interface contains:

```text
Work Form
Tables
Input fields
Save button
Send Report button
```

---

## Observed Functions

```text
saveData(event)

sendReportToClient()
```

---

## Workflow

Observed sequence:

```text
Enter Work Form Data

        |
        v

Save Data
```

Additional processing is not visible in the available files.

---

# 10.4 Contract Workflow

## Source Files

```text
ContractTemplate.html
ContractTemplate1.html
```

---

## Observed Actions

Approval:

```text
approveBtn
```

Rejection:

```text
rejectBtn
```

Additional rejection controls:

```text
اعتراض على احد البنود

اضافة او تعديل بند

رفض العقد
```

---

## Payment Interface

Observed controls:

```text
showPaymentForm()

submitPayment()

closePaymentModal()
```

Payment options visible:

```text
credit_card

sent_to_client
```

---

## Workflow Evidence

Observed interface sequence:

```text
Contract Display

        |
        v

Approve / Reject Actions

        |
        v

Payment Form Interface
```

The actual contract state management is not available.

---

# 10.5 Domain Selection Workflow

## Source Files

```text
almjal.html
almjal1.html
```

---

## Observed Navigation Steps

Visible interface contains:

```text
المجالات

التخصص

الفرع

التفصيل
```

---

## Available Actions

Buttons:

```text
الطلبات

استمارة العمل

عقد العمل

العروض
```

---

## Workflow Evidence

The HTML shows navigation between selection levels.

The data source for the tree is not identified in the available files.

---

# 10.6 Attachment Workflow

## Observed Controls

```text
attachBtn

docImageInput

addAttachmentBtn

remove-attachment
```

---

## Observed Behaviour

The interface contains controls for:

```text
Adding attachments

Removing attachments

Uploading document images
```

The storage mechanism is not available in the analyzed files.

---

# 10.7 Unknown Workflow Areas

The following workflows cannot be confirmed:

```text
User authentication

User permissions

Database saving process

Approval routing

Notifications

Payment processing backend

API communication
```

No supporting evidence was found in the analyzed HTML inventory.
```

# 11. UI Architecture

## 11.1 UI Architecture Overview

This section documents the user interface structure identified from the available HHH HTML files.

The analysis is based on:

- HTML structure.
- CSS references.
- Bootstrap usage.
- Forms.
- Tables.
- Buttons.
- Input controls.
- Page layouts.

---

# 11.2 UI Frameworks and Libraries

## Observed Libraries

The project contains a local libraries folder:

```text
hhh/libs
```

Files:

```text
bootstrap.bundle.min.js

bootstrap.rtl.min.css

jquery.min.js
```

---

## Bootstrap

Observed:

```text
bootstrap.rtl.min.css
bootstrap.bundle.min.js
```

The interface uses Bootstrap RTL styling.

---

## jQuery

Observed:

```text
jquery.min.js
```

The project includes jQuery library.

The exact usage locations require JavaScript analysis.

---

# 11.3 Page Layout Structure

Observed HTML pages contain common UI elements:

```text
Title sections

Forms

Input groups

Tables

Buttons

Panels

Accordion sections
```

---

# 11.4 Request Form Interface

## Source Pages

```text
create.html

createform.html

createorder.html

createdevided.html

AllTemplate.html
```

---

## Observed UI Components

### Form

Observed:

```text
<form id="tanjezForm">
```

---

### Input Controls

Examples:

```text
text inputs

number inputs

date inputs

select controls

textarea controls

file inputs
```

---

### Tables

Observed sections:

```text
earthBordersSection

buildingTableSection

rahaqTableSection
```

---

### Buttons

Observed examples:

```text
addBorderRow

remove-border

addBuildingRow

attachBtn

addAttachmentBtn

saveRequestBtn
```

---

# 11.5 Domain Tree Interface

## Source Pages

```text
almjal.html

almjal1.html
```

---

## Observed UI Structure

The interface contains selection levels:

```text
المجالات

التخصص

الفرع

التفصيل
```

---

## Navigation Controls

Observed buttons:

```text
back-to-main

back-to-specialty

back-to-branch

btn-requests

btn-work-form

btn-work-contract

btn-work-wofer
```

---

# 11.6 Contract Interface

## Source Pages

```text
ContractTemplate.html

ContractTemplate1.html
```

---

## Observed UI Elements

Buttons:

```text
approveBtn

rejectBtn
```

Forms:

```text
rejectForm

paymentForm
```

---

Payment interface contains:

```text
credit_card

sent_to_client
```

---

# 11.7 Work Form Interface

## Source Pages

```text
Work_Form.html

Work_Form_base.html
```

---

## Observed Components

```text
Forms

Tables

Buttons

Input fields
```

---

Observed actions:

```text
saveData(event)

sendReportToClient()
```

---

# 11.8 Responsive Design

Observed:

```html
<meta name="viewport" content="width=device-width, initial-scale=1.0">
```

The pages contain viewport configuration.

---

# 11.9 RTL Interface

Observed:

```text
bootstrap.rtl.min.css
```

The project uses Bootstrap RTL stylesheet.

---

# 11.10 UI Components Summary

| Component | Evidence |
|---|---|
| Forms | Multiple HTML forms |
| Tables | Border, building, work tables |
| Buttons | Action buttons across pages |
| Select controls | Domain and request selections |
| File upload | Attachment inputs |
| Accordion | Bootstrap collapse elements |
| RTL layout | bootstrap.rtl.min.css |

---

# 11.11 Missing UI Information

The following information is not available from the analyzed HTML files:

```text
Design system documentation

Component library

UI state management

Frontend framework

Screen prototypes

User permissions affecting UI
```
```

# 12. JavaScript Behaviour

## 12.1 JavaScript Analysis Overview

This section documents JavaScript behaviour identified from the available HHH HTML files.

The analysis is based on:

- JavaScript function names referenced in HTML.
- Button onclick attributes.
- Element IDs connected to scripts.
- Dynamic interface controls.

The complete JavaScript source files are not available in the current inventory.

---

# 12.2 Observed JavaScript Functions

## Work Form Functions

Source reference:

```text
Work_Form.html
Work_Form_base.html
```

Observed functions:

```text
saveData(event)

sendReportToClient()
```

---

## Contract Functions

Source reference:

```text
ContractTemplate.html
ContractTemplate1.html
```

Observed functions:

```text
showRejectForm()

hideRejectForm()

showPaymentForm()

closePaymentModal()

backToMethod()

submitPayment(event)
```

---

## Request Form Functions

Observed interface actions include functions related to:

```text
Adding rows

Removing rows

Adding attachments

Removing attachments

Saving form data
```

---

# 12.3 Dynamic Table Behaviour

Observed controls:

```text
addBorderRow

remove-border

addBuildingRow
```

---

## Border Table

Observed behaviour:

```text
Add border row

Remove border row
```

The exact JavaScript implementation is not available in the analyzed files.

---

## Building Table

Observed control:

```text
addBuildingRow
```

The interface contains a button for adding building rows.

---

# 12.4 Attachment Behaviour

Observed controls:

```text
attachBtn

docImageInput

addAttachmentBtn

remove-attachment
```

---

Observed interface behaviour:

```text
Open attachment input

Add attachment field

Remove attachment field
```

The upload processing logic is not available.

---

# 12.5 Name List Behaviour

Observed controls:

```text
addNameBtn1

addNameBtn2

nameList1

nameList2

nameCount1

nameCount2
```

---

Observed purpose from HTML:

```text
Adding names

Displaying name lists

Displaying name counts
```

The JavaScript implementation is not available.

---

# 12.6 Domain Navigation Behaviour

Source:

```text
almjal.html
almjal1.html
```

Observed element IDs:

```text
back-to-main

back-to-specialty

back-to-branch

btn-requests

btn-work-form

btn-work-contract

btn-work-wofer
```

---

Observed behaviour:

```text
Navigation between selection levels

Opening related sections
```

The underlying data loading mechanism is not identified.

---

# 12.7 Form Interaction Behaviour

Observed HTML interactions include:

```text
Select changes

Button clicks

Dynamic sections

Conditional visibility
```

Examples:

```text
unitCountDiv

buildingTableSection

rahaqTableSection

earthBordersSection
```

---

# 12.8 JavaScript Dependencies

Observed JavaScript library:

```text
hhh/libs/jquery.min.js

hhh/libs/bootstrap.bundle.min.js
```

---

The exact dependency usage inside scripts is not available.

---

# 12.9 Missing JavaScript Information

The following items cannot be confirmed from the available files:

```text
JavaScript source architecture

Event management structure

API calls

Database communication

Authentication logic

Validation rules

Error handling

State management
```

---

# 12.10 JavaScript Behaviour Summary

| Area | Observed Evidence |
|---|---|
| Dynamic rows | add/remove row controls |
| Attachments | attachment buttons and inputs |
| Contracts | approval, rejection, payment functions |
| Work forms | save and report functions |
| Navigation | domain tree navigation IDs |
| Libraries | jQuery and Bootstrap JavaScript |



# 13. Dependencies

## 13.1 Dependencies Analysis Overview

This section documents the dependencies identified in the HHH system files.

The analysis is based on:

- Existing files in the project directory.
- Local libraries folder.
- References visible in HTML files.

External package management files were not found in the available inventory.

---

# 13.2 Local Libraries

## Library Folder

Observed folder:

```text
hhh/libs
```

---

## Available Files

The following dependency files exist:

```text
bootstrap.bundle.min.js

bootstrap.rtl.min.css

jquery.min.js
```

---

# 13.3 Bootstrap Dependency

## Files

```text
bootstrap.bundle.min.js

bootstrap.rtl.min.css
```

---

## Observed Usage

Bootstrap is used for:

```text
RTL styling

UI components

Buttons

Forms

Tables

Accordion elements
```

The exact Bootstrap components used by JavaScript are not fully identifiable from the available analysis.

---

# 13.4 jQuery Dependency

## File

```text
jquery.min.js
```

---

## Observed Presence

The project contains jQuery as a local dependency.

The exact JavaScript functions using jQuery are not identified from the available inventory.

---

# 13.5 HTML Page Dependencies

Observed HTML pages:

```text
AllTemplate.html

almjal.html

almjal1.html

ContractTemplate.html

ContractTemplate1.html

create.html

createdevided.html

createform.html

createorder.html

createspece.html

creatform.html

devid.html

WorkOffer.html

WorkOffer1.html

Work_Form.html

Work_Form_base.html
```

---

# 13.6 File Relationships

Observed relationship indicators:

```text
Forms

Shared IDs

Repeated UI sections

Repeated Bootstrap classes

Shared local libraries
```

---

# 13.7 Python File

Observed file:

```text
localizer.py
```

Location:

```text
hhh/localizer.py
```

Size:

```text
2132 bytes
```

The purpose and execution flow of this file are not determined from the available HTML analysis.

---

# 13.8 Dependency Inventory Summary

| Dependency | Evidence |
|---|---|
| Bootstrap CSS | bootstrap.rtl.min.css |
| Bootstrap JavaScript | bootstrap.bundle.min.js |
| jQuery | jquery.min.js |
| Python file | localizer.py |
| HTML files | Multiple interface pages |

---

# 13.9 Missing Dependency Information

The following information is not available:

```text
Package manager files

NuGet packages

npm packages

Backend dependencies

Database drivers

Server dependencies

Build configuration

Deployment dependencies
```

---

# 13.10 Dependency Assessment

Based on the available files:

```text
The system uses local frontend dependencies.

The visible dependencies are mainly frontend libraries.

No backend dependency information is available in the analyzed files.
```
```

# 14. Technical Assessment

## 14.1 Assessment Overview

This section provides a technical assessment of the HHH system based on the available HTML files, local dependencies, and generated analysis documents.

The assessment is limited to the visible system structure.

No source backend code, database schema, or deployment configuration was available.

---

# 14.2 System Type Assessment

Based on the available files:

The system appears to be:

```text
HTML-based interface system

Using local frontend libraries

Containing multiple functional pages

Containing interactive forms
```

---

# 14.3 Frontend Assessment

## Observed Characteristics

The frontend contains:

```text
Forms

Tables

Input controls

Selection menus

Buttons

Dynamic sections
```

---

## UI Technology

Observed technologies:

```text
HTML

CSS

Bootstrap RTL

JavaScript

jQuery
```

---

# 14.4 Page Organization Assessment

Observed:

```text
Multiple HTML pages

Repeated interface sections

Similar form structures

Shared libraries
```

---

Assessment:

The system contains several related interface pages, but the internal organization structure cannot be fully determined without complete source analysis.

---

# 14.5 Functional Assessment

Observed functional areas:

```text
Domain selection

Request forms

Work forms

Contracts

Offers

Attachments
```

---

The available files show user interface elements related to these areas.

The complete business processing logic is not available.

---

# 14.6 Data Handling Assessment

Observed data indicators:

```text
Form field names

Input names

Entity-like references

Request objects
```

Examples:

```text
tanjezOrder

earthBorders

DocumentImage

CoordinatesX

CoordinatesY
```

---

The actual database structure cannot be confirmed from the available files.

---

# 14.7 Code Structure Assessment

Observed:

```text
Repeated HTML structures

Multiple similar pages

Embedded interface elements
```

---

Not available:

```text
Complete JavaScript architecture

Backend architecture

Database layer

API structure
```

---

# 14.8 Maintainability Assessment

Based on visible files:

Observed:

```text
Repeated page structures

Multiple HTML templates

Shared local libraries
```

---

The impact on maintainability cannot be fully assessed without:

```text
Source code organization

Backend structure

Development standards

Version history
```

---

# 14.9 Security Assessment

The available files do not provide enough information to evaluate:

```text
Authentication

Authorization

Data protection

Server security

Input validation

File security
```

---

# 14.10 Performance Assessment

Available information allows identifying:

```text
Local library usage

Multiple HTML files

Frontend resources
```

---

Performance measurement requires:

```text
Runtime testing

Network analysis

Browser profiling

Backend information
```

---

# 14.11 Integration Assessment

No confirmed integration interfaces were identified.

Not available:

```text
APIs

External services

Database connections

Third-party integrations
```

---

# 14.12 Technical Assessment Summary

| Area | Assessment |
|---|---|
| Frontend | HTML-based interface with Bootstrap and jQuery |
| Pages | Multiple functional HTML pages |
| Forms | Extensive form-based interaction |
| Libraries | Local frontend dependencies |
| Backend | Not available |
| Database | Not available |
| Security | Cannot be confirmed |
| Deployment | Not available |

---

# 14.13 Limitations

This assessment is limited to the available files:

```text
HTML files

Local libraries

Generated analysis reports
```

Additional technical conclusions require:

```text
JavaScript source files

Backend source code

Database information

Deployment files
```
```


# 15. Recommendations

## 15.1 Purpose of Recommendations

The recommendations in this section are based only on the available HHH system files and the limitations identified during reverse engineering analysis.

They do not represent a redesign proposal.

They identify the required steps to complete the system understanding.

---

# 15.2 Source Code Collection

The following files are required for a complete technical analysis:

```text
JavaScript source files

Backend source code

Database files

Configuration files

Deployment files
```

---

# 15.3 JavaScript Analysis Completion

The available analysis identified JavaScript-related elements from HTML files.

For complete behaviour documentation, the following are required:

```text
JavaScript files

Event handlers

Functions implementation

Data processing logic
```

---

# 15.4 Backend Analysis Completion

The current analysis does not include backend implementation.

Required information:

```text
Backend framework

Controllers

Models

Services

Business logic

API endpoints
```

---

# 15.5 Database Analysis Completion

The current files do not contain confirmed database structure.

Required:

```text
Database schema

Tables

Relationships

Stored procedures

Data migration files
```

---

# 15.6 Functional Documentation Expansion

The current document identifies visible interface functions.

For complete functional documentation, additional analysis is required for:

```text
Complete user workflows

Business rules

Validation rules

Data lifecycle
```

---

# 15.7 Security Documentation Completion

Security analysis requires access to:

```text
Authentication implementation

Authorization rules

Input validation

File upload handling

Data protection mechanisms
```

---

# 15.8 Deployment Documentation Completion

The current inventory does not include deployment information.

Required:

```text
Hosting environment

Server configuration

Build process

Release process
```

---

# 15.9 Final Assessment

Based on the available evidence:

```text
The HHH system contains multiple HTML-based functional interfaces.

The system includes forms, domain selection interfaces, contract interfaces, work forms, and offer-related pages.

The frontend uses Bootstrap RTL and jQuery local libraries.

The complete internal architecture cannot be confirmed without backend, database, and JavaScript source information.
```

---

# 15.10 Document Status

This reverse engineering document represents:

```text
A documentation of available system evidence.

A map of identified pages and interfaces.

A record of observed components.

A foundation for further analysis when additional source files become available.
```

---

# End of HHH System Reverse Engineering Master Document
```
