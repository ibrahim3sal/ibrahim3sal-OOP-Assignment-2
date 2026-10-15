
using SrpLab;

Console.WriteLine("=== Appointment Desk Tests ===");

// 1. Create the appointment desk
var appointmentDesk = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);

// 2. Find the next available appointment
var from = DateTimeOffset.Parse("2026-09-21T08:00:00Z");

var slot = appointmentDesk.FindNextSlot(from, 48);

if (slot is null)
{
    Console.WriteLine("No available appointment found.");
    return;
}

Console.WriteLine($"Available slot: {slot.Value}");

// 3. Book the appointment
var booked = appointmentDesk.TryBook(slot.Value);

Console.WriteLine($"Booking successful: {booked}");

// 4. Try booking the same slot again
var bookedAgain = appointmentDesk.TryBook(slot.Value);

Console.WriteLine($"Booking same slot again: {bookedAgain}");

// 5. Format an SMS reminder
var smsFormatter = new AppointmentSmsFormatter();

var reminder = smsFormatter.Format(slot.Value, "0100");

Console.WriteLine("\n=== SMS Reminder ===");
Console.WriteLine(reminder);

// 6. Generate an ICS calendar event
var calendarExporter = new AppointmentCalendarExporter();

var calendar = calendarExporter.ToIcs( slot.Value, appointmentDesk.SlotMinutes, "Ali", "Dr. Ahmed");

Console.WriteLine("\n=== ICS Calendar ===");
Console.WriteLine(calendar);



Console.WriteLine("\n=== Checkout Basket Tests ===");

var basket = new CheckoutBasket();

basket.AddLine("SKU-1", 40m, 2);
basket.ApplyCouponText("SAVE10");
basket.EnableGiftWrap();

Console.WriteLine($"Subtotal: {basket.SubTotal():0.00}");
Console.WriteLine($"Discount: {basket.DiscountAmount():0.00}");
Console.WriteLine($"Grand total: {basket.GrandTotal():0.00}");

Console.WriteLine(basket.GiftMessageCard("Ali"));

Console.WriteLine(basket.AuthorizePaymentStub("4242"));



Console.WriteLine("\n=== Course Enrollment Tests ===");

var course = new CourseEnrollmentDesk("SEF-101", capacity: 1, tuition: 3000m);

Console.WriteLine(course.Register("a@mail.com"));
Console.WriteLine(course.Register("b@mail.com"));

Console.WriteLine(course.WelcomePacketMarkdown("b@mail.com", "Bea"));

Console.WriteLine(course.TuitionInvoiceLine("a@mail.com"));

course.PromoteFromWaitlist(1);

Console.WriteLine(course.TuitionInvoiceLine("b@mail.com"));



Console.WriteLine("\n=== Grade Book Tests ===");

var grades = new GradeBook();
grades.Record("s1", 92);
grades.Record("s1", 88);
grades.Record("s2", 70);

var gradePolicy = new GradePolicy();
var gradeReportFormatter = new GradeReportFormatter(gradePolicy);

var average = grades.Average("s1");

Console.WriteLine($"Average: {average}");
Console.WriteLine($"Letter: {gradePolicy.Letter(average)}");
Console.WriteLine($"Honor Roll: {gradePolicy.MeetsHonorRoll(average)}");

Console.WriteLine(gradeReportFormatter.TranscriptPlain("s1", "Ali", average));

Console.WriteLine(gradeReportFormatter.ExportCsv(grades));



Console.WriteLine("\n=== Kitchen Ticket Tests ===");

var kitchen = new KitchenTicket();

kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
kitchen.AddItem("Salad", new[] { "lettuce", "tomato" }, 5);

var allergenDetector = new AllergenDetector();
var preparationEstimator = new KitchenPreparationEstimator();
var ticketFormatter = new KitchenTicketFormatter();

var allergens = allergenDetector.Detect(kitchen.Items);

var eta = preparationEstimator.EstimateReadyMinutes(kitchen.Items, openStations: 2, allergenCount: allergens.Count);

var lane = preparationEstimator.DetermineExpoLane(allergens.Count, eta);

Console.WriteLine("Allergens: " + string.Join(", ", allergens));
Console.WriteLine($"Estimated ready: {eta} minutes");
Console.WriteLine($"Expo lane: {lane}");

Console.WriteLine(ticketFormatter.RenderThermalTicket(42, kitchen.Items, allergens, eta));


    
Console.WriteLine("\n=== Loan Desk Tests ===");

var loan = new LoanDesk(60_000m, 640, 4, hasCollateral: false);

var riskCalculator = new LoanRiskCalculator();
var documentProvider = new LoanDocumentProvider();
var letterFormatter = new LoanDecisionLetterFormatter();
var csvFormatter = new LoanApplicationCsvFormatter();

var riskScore = riskCalculator.RiskScore(loan.RequestedAmount, loan.CreditScore, loan.EmploymentMonths, loan.HasCollateral);

var isEligible = riskCalculator.IsEligible(loan.RequestedAmount, loan.CreditScore, loan.EmploymentMonths, loan.HasCollateral);

var documents = documentProvider.RequiredDocuments(loan.RequestedAmount, loan.EmploymentMonths, loan.HasCollateral, isEligible);

Console.WriteLine($"Risk score: {riskScore}");
Console.WriteLine($"Eligible: {isEligible}");
Console.WriteLine("Required documents:");

foreach (var document in documents)
{
    Console.WriteLine($"- {document}");
}

Console.WriteLine(letterFormatter.Format("Omar", loan.RequestedAmount, riskScore, isEligible, documents));

Console.WriteLine(csvFormatter.Format("APP-1", loan.CreditScore, loan.EmploymentMonths, loan.HasCollateral, riskScore, isEligible));



Console.WriteLine("\n=== Subscription Billing Tests ===");

var subscription = new SubscriptionBilling("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));

subscription.RegisterFailedPayment();

var prorationCalculator = new SubscriptionProrationCalculator();
var invoiceGenerator = new InvoiceNumberGenerator();
var emailFormatter = new DunningEmailFormatter();
var ledgerFormatter = new LedgerJournalFormatter();

var amount = prorationCalculator.Calculate(subscription.MonthlyPrice, subscription.PeriodStart, subscription.PeriodEnd, subscription.PeriodStart);

var invoiceNumber = invoiceGenerator.NextInvoiceNumber(subscription.PeriodStart);

Console.WriteLine($"Prorated amount: {amount:0.00}");

Console.WriteLine(emailFormatter.Format("Sara", new DateOnly(2026, 9, 20), amount, subscription.FailedPayments, invoiceNumber));

Console.WriteLine(ledgerFormatter.Format(subscription.CustomerId, invoiceGenerator.NextInvoiceNumber(subscription.PeriodStart), amount));


        
Console.WriteLine("\n=== Support Ticket Tests ===");

var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);

var slaCalculator = new TicketSlaCalculator();
var messageFormatter = new SupportMessageFormatter();

var deadline = slaCalculator.Deadline(ticket.OpenedAt, ticket.Priority);

Console.WriteLine($"Priority: {ticket.Priority}");
Console.WriteLine($"SLA deadline: {deadline:u}");
Console.WriteLine($"SLA breached: {slaCalculator.IsBreached(ticket.OpenedAt, ticket.Priority, DateTimeOffset.UtcNow)}");

Console.WriteLine(messageFormatter.DraftPublicReply(ticket.Id, ticket.Priority, "Nora", deadline));

Console.WriteLine(messageFormatter.InternalEscalationBlurb(ticket.Id, ticket.Priority, deadline));



Console.WriteLine("\n=== Ward Board Tests ===");

var ward = new WardBoard();

ward.AssignBed(1, "p-88", heartRate: 130, spo2: 89);

Console.WriteLine(ward.BuildHandoffNote(1));

Console.WriteLine(string.Join(" | ", ward.DrainPagerLog()));

Console.WriteLine(ward.ExportCensusCsv());



Console.WriteLine("\n=== Warehouse Pick List Tests ===");

var pickList = new WarehousePickList();

pickList.AddNeed("BOLT", "A", 3, 10, 7);
pickList.AddNeed("NUT", "B", 1, 5, 5);

Console.WriteLine(pickList.PickerScript());

Console.WriteLine(pickList.WmsXmlBatch("BATCH-1"));