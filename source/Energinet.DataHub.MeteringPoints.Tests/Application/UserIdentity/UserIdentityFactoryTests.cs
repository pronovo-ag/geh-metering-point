﻿// Copyright 2020 Energinet DataHub A/S
//
// Licensed under the Apache License, Version 2.0 (the "License2");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Energinet.DataHub.MeteringPoints.Application.UserIdentity;
using Xunit;

namespace Energinet.DataHub.MeteringPoints.Tests.Application.UserIdentity
{
    public class UserIdentityFactoryTests
    {
        [Theory]
        [InlineData("{\"geh_userIdentity\":\"{\\\"Id\\\":\\\"5\\\"}\"}", "geh_userIdentity", "5")]
        [InlineData("{\"other_userIdentity\":\"{\\\"Id\\\":\\\"5\\\"}\", \"geh_userIdentity\":\"{\\\"Id\\\":\\\"5\\\"}\"}", "geh_userIdentity", "5")]
        public void ConvertToUserIdentityFromDictionaryString(string inputText, string propertyKey, string expectedUserId)
        {
            // Arrange
            var userIdentityFactory = new UserIdentityFactory();

            // Act
            var userIdentityParsed = userIdentityFactory.FromDictionaryString(inputText, propertyKey);

            // Assert
            Assert.Equal(expectedUserId, userIdentityParsed.Id);
        }

        [Theory]
        [InlineData("{\"123geh_userIdentity\":\"{\\\"Id\\\":\\\"5\\\"}\"}", "geh_userIdentity")]
        public void UserIdentityArgumentNullException(string inputText, string propertyKey)
        {
            // Arrange
            var userIdentityFactory = new UserIdentityFactory();

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() =>
                userIdentityFactory.FromDictionaryString(inputText, propertyKey));
        }
    }
}
