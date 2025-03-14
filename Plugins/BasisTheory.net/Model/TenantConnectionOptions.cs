/*
 * Basis Theory API
 *
 * ## Getting Started * Sign-in to [Basis Theory](https://basistheory.com) and go to [Applications](https://portal.basistheory.com/applications) * Create a Basis Theory Private Application * All permissions should be selected * Paste the API Key into the `BT-API-KEY` variable
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = BasisTheory.net.Client.OpenAPIDateConverter;

namespace BasisTheory.net.Model
{
    /// <summary>
    /// TenantConnectionOptions
    /// </summary>
    [DataContract(Name = "TenantConnectionOptions")]
    public partial class TenantConnectionOptions : IEquatable<TenantConnectionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantConnectionOptions" /> class.
        /// </summary>
        /// <param name="domainAliases">domainAliases.</param>
        public TenantConnectionOptions(List<string> domainAliases = default(List<string>))
        {
            this.DomainAliases = domainAliases;
        }

        /// <summary>
        /// Gets or Sets DomainAliases
        /// </summary>
        [DataMember(Name = "domain_aliases", EmitDefaultValue = true)]
        public List<string> DomainAliases { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TenantConnectionOptions {\n");
            sb.Append("  DomainAliases: ").Append(DomainAliases).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as TenantConnectionOptions);
        }

        /// <summary>
        /// Returns true if TenantConnectionOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of TenantConnectionOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TenantConnectionOptions input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.DomainAliases == input.DomainAliases ||
                    this.DomainAliases != null &&
                    input.DomainAliases != null &&
                    this.DomainAliases.SequenceEqual(input.DomainAliases)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.DomainAliases != null)
                {
                    hashCode = (hashCode * 59) + this.DomainAliases.GetHashCode();
                }
                return hashCode;
            }
        }

    }

}
