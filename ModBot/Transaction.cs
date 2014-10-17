using System;
using System.Globalization;
namespace ModBot
{
    public class Transaction
    {
        public string id, amount, donor, notes;
        public DateTime date;

        public Transaction(string id, DateTime date, string amount, string donor, string notes)
        {
            this.id = id;
            this.date = date;
            this.amount = amount;
            this.donor = donor;
            this.notes = notes;
        }

        // Summary:
        //     A format to print the details of the transaction in:
        //     TRANSACTIONID/ID/TRANSACTION for the ID.
        //     DATE/TIME for the accurate date.
        //     DATE_ROUND for the rounded date (no miliseconds).
        //     AMOUNT for the amount donated.
        //     DONOR/NAME for the donor's name.
        //     NOTES for the notes.
        /// <summary>
        /// Converts a transaction to a string
        /// </summary>
        /// <param name="format">TRANSACTIONID/ID/TRANSACTION for the ID.
        ///     DATE/TIME for the accurate date.
        ///     DATE_ROUND for the rounded date (no miliseconds).
        ///     AMOUNT for the amount donated.
        ///     DONOR/NAME for the donor's name.
        ///     NOTES for the notes.</param>
        /// <returns>A string of the transaction according to the format</returns>
        public string ToString(string format)
        {
            return format.Replace("TRANSACTIONID", id).Replace("ID", id).Replace("TRANSACTION", id).Replace("DATETIME", date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern)).Replace("TIME", date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern)).Replace("DATE", date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)).Replace("AMOUNT", amount).Replace("DONOR", donor).Replace("NAME", donor).Replace("NOTES", notes);
        }
    }
}
