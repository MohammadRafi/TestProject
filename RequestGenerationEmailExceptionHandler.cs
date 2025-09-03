private string GetOrderSummaryForEmailBody(Request request)
{
    try
    {
        if (request == null)
            return string.Empty;

        IEnumerable<string> orderAndSectionDetails = request.SectionType switch
        {
            RequestSectionType.Current => request.CurrentTaxes?
                .Where(y => y?.Order != null)
                .OrderBy(y => y.Order.Number)
                .Select(x => $"{x.Order.Number} {Constants.TaxCollection}")
                ?? Enumerable.Empty<string>(),

            RequestSectionType.Delinquent => request.PriorTaxes?
                .Where(y => y?.Order != null)
                .OrderBy(y => y.Order.Number)
                .Select(x => $"{x.Order.Number} - {Constants.DelinquentCollection}")
                ?? Enumerable.Empty<string>(),

            RequestSectionType.Utility => request.Utilities?
                .Where(y => y?.Order != null)
                .OrderBy(y => y.Order.Number)
                .Select(x => $"{x.Order.Number} - {Constants.UtilitySectionName}")
                ?? Enumerable.Empty<string>(),

            RequestSectionType.GeneralRequest => request.GeneralRequests?
                .Where(y => y?.Order != null)
                .OrderBy(y => y.Order.Number)
                .Select(x => $"{x.Order.Number} {Constants.GeneralRequestSectionName}")
                ?? Enumerable.Empty<string>(),

            _ => Enumerable.Empty<string>()
        };

        // Ensure no nulls in the result
        orderAndSectionDetails = orderAndSectionDetails.Where(item => !string.IsNullOrWhiteSpace(item));

        return string.Join("<br>", orderAndSectionDetails.Distinct());
    }
    catch (Exception ex)
    {
        // Log exception if you have logging (ex.Message, ex.StackTrace, etc.)
        return string.Empty; // Safe fallback
    }
}
