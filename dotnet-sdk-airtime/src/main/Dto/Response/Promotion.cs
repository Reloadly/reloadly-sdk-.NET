using System;

namespace Reloadly.Airtime.Dto.Response
{
    public class Promotion
    {
        /// <summary>
        /// Unique identifier for the given promotion
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id of operator to which the promotion applies
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// Title of the promotion
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// 2nd title for the promotion if any
        /// </summary>
        public string Title2 { get; set; } = default!;

        /// <summary>
        /// Description of the promotion
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Date on which the promotion starts
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Date on which the promotion ends
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Amounts for which the promotion applies
        /// </summary>
        public string Denominations { get; set; } = default!;

        /// <summary>
        /// Amounts (in destination country currency) for which the promotion applies
        /// </summary>
        public string LocalDenominations { get; set; } = default!;
    }
}