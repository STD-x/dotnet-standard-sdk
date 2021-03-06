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

using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using IBM.WatsonDeveloperCloud.Service;
using System;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2
{
    public class LanguageTranslatorService : WatsonService, ILanguageTranslatorService
    {
        const string SERVICE_NAME = "language_translator";
        const string URL = "https://gateway.watsonplatform.net/language-translator/api";
        public LanguageTranslatorService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public LanguageTranslatorService(string userName, string password) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);

        }

        public LanguageTranslatorService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Translate. Translates the input text from the source language to the target language.
        /// </summary>
        /// <param name="request">The translate request containing the text, and either a model ID or source and target language pair.</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        public TranslationResult Translate(TranslateRequest translateRequest)
        {
            if (translateRequest == null)
                throw new ArgumentNullException(nameof(translateRequest));
            TranslationResult result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v2/translate");
                request.WithBody<TranslateRequest>(translateRequest);
                result = request.As<TranslationResult>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Identify language. Identifies the language of the input text.
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        public IdentifiedLanguages Identify(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            IdentifiedLanguages result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v2/identify");
                request.WithBodyContent(new StringContent(text, Encoding.UTF8, HttpMediaType.TEXT_PLAIN));
                result = request.As<IdentifiedLanguages>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Identify language. Identifies the language of the input text.
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        public IdentifiedLanguages IdentifyAsPlain(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            IdentifiedLanguages result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v2/identify");
                request.WithBodyContent(new StringContent(text, Encoding.UTF8, HttpMediaType.TEXT_PLAIN));
                result = request.As<IdentifiedLanguages>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List identifiable languages. Lists the languages that the service can identify. Returns the language code (for example, `en` for English or `es` for Spanish) and name of each language.
        /// </summary>
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        public IdentifiableLanguages ListIdentifiableLanguages()
        {
            IdentifiableLanguages result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v2/identifiable_languages");
                result = request.As<IdentifiableLanguages>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create model. Uploads a TMX glossary file on top of a domain to customize a translation model.  Depending on the size of the file, training can range from minutes for a glossary to several hours for a large parallel corpus. Glossary files must be less than 10 MB. The cumulative file size of all uploaded glossary and corpus files is limited to 250 MB.
        /// </summary>
        /// <param name="baseModelId">The model ID of the model to use as the base for customization. To see available models, use the `List models` method.</param>
        /// <param name="name">An optional model name that you can use to identify the model. Valid characters are letters, numbers, dashes, underscores, spaces and apostrophes. The maximum length is 32 characters. (optional)</param>
        /// <param name="forcedGlossary">A TMX file with your customizations. The customizations in the file completely overwrite the domain translaton data, including high frequency or high confidence phrase translations. You can upload only one glossary with a file size less than 10 MB per call. (optional)</param>
        /// <param name="parallelCorpus">A TMX file that contains entries that are treated as a parallel corpus instead of a glossary. (optional)</param>
        /// <param name="monolingualCorpus">A UTF-8 encoded plain text file that is used to customize the target language model. (optional)</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public TranslationModel CreateModel(string baseModelId, string name = null, System.IO.Stream forcedGlossary = null, System.IO.Stream parallelCorpus = null, System.IO.Stream monolingualCorpus = null)
        {
            if (string.IsNullOrEmpty(baseModelId))
                throw new ArgumentNullException(nameof(baseModelId));
            TranslationModel result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (forcedGlossary != null)
                {
                    var forcedGlossaryContent = new ByteArrayContent((forcedGlossary as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    forcedGlossaryContent.Headers.ContentType = contentType;
                    formData.Add(forcedGlossaryContent, "forced_glossary", "filename");
                }

                if (parallelCorpus != null)
                {
                    var parallelCorpusContent = new ByteArrayContent((parallelCorpus as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    parallelCorpusContent.Headers.ContentType = contentType;
                    formData.Add(parallelCorpusContent, "parallel_corpus", "filename");
                }

                if (monolingualCorpus != null)
                {
                    var monolingualCorpusContent = new ByteArrayContent((monolingualCorpus as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/plain", out contentType);
                    monolingualCorpusContent.Headers.ContentType = contentType;
                    formData.Add(monolingualCorpusContent, "monolingual_corpus", "filename");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v2/models");
                if (!string.IsNullOrEmpty(baseModelId))
                    request.WithArgument("base_model_id", baseModelId);
                if (!string.IsNullOrEmpty(name))
                    request.WithArgument("name", name);
                request.WithBodyContent(formData);
                result = request.As<TranslationModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete model. Deletes a custom translation model.
        /// </summary>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        public DeleteModelResult DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));
            DeleteModelResult result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v2/models/{modelId}");
                result = request.As<DeleteModelResult>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get model details. Gets information about a translation model, including training status for custom models.
        /// </summary>
        /// <param name="modelId">Model ID of the model to get.</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public TranslationModel GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));
            TranslationModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v2/models/{modelId}");
                result = request.As<TranslationModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List models. Lists available translation models.
        /// </summary>
        /// <param name="source">Specify a language code to filter results by source language. (optional)</param>
        /// <param name="target">Specify a language code to filter results by target language. (optional)</param>
        /// <param name="defaultModels">If the default parameter isn't specified, the service will return all models (default and non-default) for each language pair. To return only default models, set this to `true`. To return only non-default models, set this to `false`. (optional)</param>
        /// <returns><see cref="TranslationModels" />TranslationModels</returns>
        public TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null)
        {
            TranslationModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v2/models");
                if (!string.IsNullOrEmpty(source))
                    request.WithArgument("source", source);
                if (!string.IsNullOrEmpty(target))
                    request.WithArgument("target", target);
                if (defaultModels != null)
                    request.WithArgument("default", defaultModels);
                result = request.As<TranslationModels>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
