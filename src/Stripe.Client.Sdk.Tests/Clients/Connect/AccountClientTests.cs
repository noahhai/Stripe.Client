﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Clients.Connect;
using Stripe.Client.Sdk.Models;
using Stripe.Client.Sdk.Models.Arguments;
using Stripe.Client.Sdk.Models.Filters;
using System.Threading;
using System.Threading.Tasks;

namespace Stripe.Client.Sdk.Tests.Clients.Connect
{
    [TestClass]
    public class AccountClientTests
    {
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private IAccountClient _client;
        private IStripeClient _stripe;

        [TestInitialize]
        public void Init()
        {
            _stripe = Substitute.For<IStripeClient>();
            _client = new AccountClient(_stripe);
        }

        [TestMethod]
        public async Task GetAccountTest()
        {
            // Arrange
            var id = "account-id";
            _stripe.Get(Arg.Is<StripeRequest<Account>>(a => a.UrlPath == "accounts/" + id), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Account>()));

            // Act
            var response = await _client.GetAccount(id, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetAccountsTest()
        {
            // Arrange
            var filter = new AccountListFilter();
            _stripe.Get(
                Arg.Is<StripeRequest<AccountListFilter, Pagination<Account>>>(
                    a => a.UrlPath == "accounts" && a.Model == filter), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Pagination<Account>>()));

            // Act
            var response = await _client.GetAccounts(filter, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task CreateAccountTest()
        {
            // Arrange
            var args = new AccountCreateArguments();
            _stripe.Post(
                Arg.Is<StripeRequest<AccountCreateArguments, Account>>(a => a.UrlPath == "accounts" && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Account>()));

            // Act
            var response = await _client.CreateAccount(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateAccountTest()
        {
            // Arrange
            var args = new AccountUpdateArguments
            {
                AccountId = "account-id"
            };
            _stripe.Post(
                Arg.Is<StripeRequest<AccountUpdateArguments, Account>>(
                    a => a.UrlPath == "accounts/" + args.AccountId && a.Model == args), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Account>()));

            // Act
            var response = await _client.UpdateAccount(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetBankAccountTest()
        {
            // Arrange
            var accountId = "account-id";
            var id = "bank-account-id";
            _stripe.Get(
                Arg.Is<StripeRequest<BankAccount>>(
                    a => a.UrlPath == "accounts/" + accountId + "/external_accounts/" + id), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<BankAccount>()));

            // Act
            var response = await _client.GetBankAccount(id, accountId, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetBankAccountsTest()
        {
            // Arrange
            var filter = new AccountBankAccountListFilter
            {
                AccountId = "account-id"
            };
            _stripe.Get(
                Arg.Is<StripeRequest<AccountBankAccountListFilter, Pagination<BankAccount>>>(
                    a => a.UrlPath == "accounts/" + filter.AccountId + "/external_accounts" && a.Model == filter),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Pagination<BankAccount>>()));

            // Act
            var response = await _client.GetBankAccounts(filter, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task CreateBankAccountTest()
        {
            // Arrange
            var args = new AccountBankAccountCreateArguments
            {
                AccountId = "account-id"
            };
            _stripe.Post(
                Arg.Is<StripeRequest<AccountBankAccountCreateArguments, BankAccount>>(
                    a => a.UrlPath == "accounts/" + args.AccountId + "/external_accounts" && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<BankAccount>()));

            // Act
            var response = await _client.CreateBankAccount(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateBankAccountTest()
        {
            // Arrange
            var args = new AccountBankAccountUpdateArguments
            {
                BankAccountId = "some-value",
                AccountId = "account-id"
            };
            _stripe.Post(
                Arg.Is<StripeRequest<AccountBankAccountUpdateArguments, BankAccount>>(
                    a => a.UrlPath == "accounts/" + args.AccountId + "/external_accounts/" + args.BankAccountId && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<BankAccount>()));

            // Act
            var response = await _client.UpdateBankAccount(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetCardTest()
        {
            // Arrange
            var accountId = "ACC_1234";
            var id = "ID_VALUE";
            _stripe.Get(
                Arg.Is<StripeRequest<Card>>(a => a.UrlPath == "accounts/" + accountId + "/external_accounts/" + id),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Card>()));

            // Act
            var response = await _client.GetCard(id, accountId, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetCardsTest()
        {
            // Arrange
            var filter = new AccountCardListFilter
            {
                AccountId = "account-id"
            };
            _stripe.Get(
                Arg.Is<StripeRequest<AccountCardListFilter, Pagination<Card>>>(
                    a => a.UrlPath == "accounts/" + filter.AccountId + "/external_accounts" && a.Model == filter),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Pagination<Card>>()));

            // Act
            var response = await _client.GetCards(filter, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task CreateCardTest()
        {
            // Arrange
            var args = new AccountCardCreateArguments
            {
                AccountId = "account-id"
            };
            _stripe.Post(
                Arg.Is<StripeRequest<AccountCardCreateArguments, Card>>(
                    a => a.UrlPath == "accounts/" + args.AccountId + "/external_accounts" && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Card>()));

            // Act
            var response = await _client.CreateCard(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateCardTest()
        {
            // Arrange
            var args = new AccountCardUpdateArguments
            {
                CardId = "card-id",
                AccountId = "account-id"
            };
            _stripe.Post(
                Arg.Is<StripeRequest<AccountCardUpdateArguments, Card>>(
                    a => a.UrlPath == "accounts/" + args.AccountId + "/external_accounts/" + args.CardId && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Card>()));

            // Act
            var response = await _client.UpdateCard(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }
    }
}