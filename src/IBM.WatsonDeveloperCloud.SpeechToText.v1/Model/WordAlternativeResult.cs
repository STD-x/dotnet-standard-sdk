/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// WordAlternativeResult.
    /// </summary>
    public class WordAlternativeResult
    {
        /// <summary>
        /// A confidence score for the word alternative hypothesis in the range of 0 to 1.
        /// </summary>
        /// <value>A confidence score for the word alternative hypothesis in the range of 0 to 1.</value>
        [JsonProperty("confidence", NullValueHandling = NullValueHandling.Ignore)]
        public double? Confidence { get; set; }
        /// <summary>
        /// An alternative hypothesis for a word from the input audio.
        /// </summary>
        /// <value>An alternative hypothesis for a word from the input audio.</value>
        [JsonProperty("word", NullValueHandling = NullValueHandling.Ignore)]
        public string Word { get; set; }
    }

}
