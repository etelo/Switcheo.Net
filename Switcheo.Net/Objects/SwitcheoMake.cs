﻿using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Switcheo.Net.Converters;
using System;

namespace Switcheo.Net.Objects
{
    /// <summary>
    /// Information about a make
    /// When an <see cref="SwitcheoOrder"/> is created, the order matching engine will match the order against existing offers
    /// If the order is not fully filled by existing offers, a <see cref="SwitcheoMake"/> is created. This <see cref="SwitcheoMake"/> represents the unfilled amount of the order.
    /// </summary>
    public class SwitcheoMake
    {
        /// <summary>
        /// The make id generated by Switcheo
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The offer hash
        /// </summary>
        [JsonProperty("offer_hash")]
        public string OfferHash { get; set; }

        [JsonProperty("available_amount")]
        private string _AvailableAmount { get; set; }

        /// <summary>
        /// Remaining amount of the offered tokens in the make that has not been filled by other offers
        /// </summary>
        [JsonIgnore]
        public decimal AvailableAmount
        {
            get
            {
                return SwitcheoHelpers.FromAssetAmount(this._AvailableAmount, this.OfferAsset?.Precision);
            }
        }

        /// <summary>
        /// Asset of the token that the make is offering
        /// </summary>
        [JsonProperty("offer_asset_id")]
        [JsonConverter(typeof(TokenConverter), true)]
        public SwitcheoToken OfferAsset { get; set; }

        [JsonProperty("offer_amount")]
        private string _OfferAmount { get; set; }

        /// <summary>
        /// Amount of tokens that the make is offering
        /// </summary>
        [JsonIgnore]
        public decimal OfferAmount
        {
            get
            {
                return SwitcheoHelpers.FromAssetAmount(this._OfferAmount, this.OfferAsset?.Precision);
            }
        }

        /// <summary>
        /// Asset of the token that the make wants
        /// </summary>
        [JsonProperty("want_asset_id")]
        [JsonConverter(typeof(TokenConverter), true)]
        public SwitcheoToken WantAsset { get; set; }

        [JsonProperty("want_amount")]
        private string _WantAmount { get; set; }

        /// <summary>
        /// Amount of tokens that the make wants
        /// </summary>
        [JsonIgnore]
        public decimal WantAmount
        {
            get
            {
                return SwitcheoHelpers.FromAssetAmount(this._WantAmount, this.WantAsset?.Precision);
            }
        }

        [JsonProperty("filled_amount")]
        private string _FilledAmount { get; set; }

        /// <summary>
        /// Amount of tokens out of the make's OfferAmount that has been taken by other orders.
        /// </summary>
        [JsonIgnore]
        public decimal FilledAmount
        {
            get
            {
                return SwitcheoHelpers.FromAssetAmount(this._FilledAmount, this.OfferAsset?.Precision);
            }
        }

        /// <summary>
        /// The transaction representing this make
        /// </summary>
        [JsonProperty("txn")]
        public SwitcheoTransaction Transaction { get; set; }

        /// <summary>
        /// If this make was cancelled, this parameter would be the transaction that represents the cancellation
        /// </summary>
        [JsonProperty("cancel_txn")]
        public SwitcheoTransaction CancelTransaction { get; set; }

        /// <summary>
        /// Buy or sell price of order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Status of the make
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(MakeStatusConverter))]
        public MakeStatus Status { get; set; }

        /// <summary>
        /// Time when the make was created
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(UTCDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Transaction hash of the transaction representing this make
        /// </summary>
        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        /// <summary>
        /// Trades associated with this make
        /// </summary>
        [JsonProperty("trades")]
        public SwitcheoTrade[] Trades { get; set; }

        public override string ToString()
        {
            return string.Format("{{ Id : {0}, OfferHash : {1}, AvailableAmount : {2}, OfferAsset : {3}, OfferAmount : {4}, WantAsset : {5}, WantAmount : {6}, FilledAmount : {7}, Transaction : {8}, CancelTransaction : {9}, Price : {10}, Status : {11}, CreatedAt : {12}, TransactionHash : {13}, Trades : {14} }}",
                this.Id.ToString(), this.OfferHash, this.AvailableAmount, this.OfferAsset.ToString(), this.OfferAmount, this.WantAsset.ToString(),
                this.WantAmount, this.FilledAmount, this.Transaction != null ? this.Transaction.ToString() : "null",
                this.CancelTransaction != null ? this.CancelTransaction.ToString() : "null",
                this.Price, this.Status, this.CreatedAt.ToString(), this.TransactionHash,
                this.Trades != null ? $"(Length : {this.Trades.Length})" : "null");
        }
    }
}
