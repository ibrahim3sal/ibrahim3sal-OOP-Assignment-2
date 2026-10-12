# Responsibilities Analysis

## 1. AppointmentDesk

**Responsibilities:**
- Managing appointment availability and bookings.
- Generating calendar content in ICS format.
- Formatting SMS reminders.

**Why this violates SRP:**
The class handles scheduling, booking, calendar serialization, and SMS formatting. These responsibilities may change for different reasons.

## 2. CheckoutBasket

**Responsibilities:**
- Managing basket items and quantities.
- Calculating subtotals, discounts, and grand totals.
- Managing gift-wrapping options and fees.
- Generating gift message cards.
- Generating mock payment authorization codes.

**Why this violates SRP:**
The class combines basket management and pricing with gift-message generation and payment authorization.

## 3. CourseEnrollmentDesk

**Responsibilities:**
- Managing student enrollment, seat capacity, and waitlists.
- Promoting students from the waitlist.
- Generating welcome packets in Markdown.
- Formatting tuition invoice lines, including VAT.

**Why this violates SRP:**
The class combines enrollment management with document generation and invoice formatting.

## 4. GradeBook

**Responsibilities:**
- Recording student scores and calculating averages.
- Assigning letter grades and determining honor-roll eligibility.
- Generating plain-text transcripts.
- Exporting grade data to CSV.

**Why this violates SRP:**
The class handles grade management and evaluation as well as transcript generation and CSV serialization.

## 5. KitchenTicket

**Responsibilities:**
- Managing order items.
- Detecting allergens.
- Estimating preparation time and determining the expo lane.
- Formatting thermal kitchen tickets.

**Why this violates SRP:**
The class combines order management, allergen analysis, preparation planning, and ticket formatting.

## 6. LoanDesk

**Responsibilities:**
- Calculating risk scores and determining loan eligibility.
- Determining required documents.
- Generating loan decision letters.
- Formatting underwriter data as CSV.

**Why this violates SRP:**
The class combines risk assessment and eligibility rules with document generation and CSV formatting.

## 7. SubscriptionBilling

**Responsibilities:**
- Calculating prorated subscription charges.
- Generating invoice numbers.
- Tracking failed payments.
- Generating payment reminder emails.
- Formatting accounting ledger entries.

**Why this violates SRP:**
The class combines billing calculations and payment tracking with email generation, invoice numbering, and accounting output formatting.

## 8. SupportTicket

**Responsibilities:**
- Managing ticket information and customer messages.
- Determining ticket priority from text.
- Calculating SLA deadlines and checking whether tickets have breached them.
- Generating public replies and internal escalation messages.

**Why this violates SRP:**
The class combines ticket management, priority rules, SLA calculations, and message generation.

## 9. WardBoard

**Responsibilities:**
- Managing bed assignments and patient information.
- Calculating patient acuity scores.
- Generating patient handoff notes.
- Managing pager alerts and their log.
- Exporting patient census data to CSV.

**Why this violates SRP:**
The class handles bed and patient management, clinical scoring, alert logging, report generation, and CSV export.

## 10. WarehousePickList

**Responsibilities:**
- Managing requested items and quantities.
- Allocating available stock to pick-list items.
- Calculating the walking order for collecting items.
- Generating picker instructions and shortage information.
- Exporting pick-list data as WMS XML.

**Why this violates SRP:**
The class combines item management and stock allocation with route ordering, instruction generation, and XML serialization.