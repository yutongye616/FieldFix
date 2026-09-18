# FieldFix Power Platform Blueprint

## Overview
FieldFix is a Microsoft Power Platform solution for managing field service and maintenance requests. It is designed for operations teams that need to create work orders, assign technicians, and monitor ticket status in near real time.

## Solution components

### Dataverse tables
1. Service Request
   - Title
   - Description
   - Location
   - Requestor
   - Priority
   - Status
   - Category
   - Created Date
   - Assigned Technician

2. Technician
   - Name
   - Shift
   - Skill Set
   - Active Status
   - Assigned Zone

3. Asset / Location
   - Asset Name
   - Asset Type
   - Zone
   - Facility ID
   - Last Inspection Date

4. Work Order
   - Service Request ID
   - Technician ID
   - Scheduled Time
   - Completion Time
   - Resolution Notes
   - Work Status

### Canvas app screens
- Dashboard: summary cards for open items, urgent tickets, and SLA risk
- New Request: form for submitting a reported issue
- Dispatch Queue: list of active requests sorted by priority
- Technician View: work assigned to a technician by shift
- Detail Screen: full request and notes history

### Power Automate flows
- Send automatic email when a request is created
- Notify supervisor when priority is High or Critical
- Escalate overdue open work orders after SLA threshold
- Update status when a technician completes a work order

### Security model
- Operations Manager role: full access to dashboard and assignments
- Dispatcher role: create and assign tickets
- Technician role: view assigned work orders and update status
- Basic user role: submit a request without editing records

## App experience
The app should feel like a simple operations command center:
- one screen for submitting issues
- one screen for assigning and tracking work
- one screen for live status monitoring

## Deployment steps
1. Create a Power Platform environment.
2. Create the Dataverse tables and relationships.
3. Import or configure the app with the Canvas App designer.
4. Build Power Automate flows and approvals.
5. Add security roles and environment users.
6. Publish the app and validate on mobile and desktop.

## Suggested functional flow
- A user submits a request from the mobile app.
- The system captures location, category, and priority.
- Dispatcher reviews and assigns to a technician.
- Technician updates status as work progresses.
- Completed work closes the ticket and triggers notification.

## Why this fits the role
This approach matches skills in HTML, CSS, JavaScript, C#, and Power Apps development by showing how a low-code solution can be paired with a custom .NET web app for complex reporting and enterprise hosting.
