﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Bogus;
using Bogus.DataSets;
using static Bogus.DataSets.Name;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Helpers
{
	public static class BogusGenerator
	{
         
		private   const string Locale= "en";
		public static readonly DataSet DataSet = new DataSet(Locale);
		public static readonly Faker Faker = new Faker(Locale);
		public static readonly Person Person = new Person(Locale);
		public static readonly Address Address = new Address(Locale);
		public static readonly Commerce Commerce = new Commerce(Locale);
		public static readonly Company Company = new Company(Locale);
		public static readonly Bogus.DataSets.Database Database = new Bogus.DataSets.Database();
		public static readonly Date Date = new Date(Locale);
		public static readonly Finance Finance = new Finance();
		public static readonly Hacker Hacker = new Hacker(Locale);
		public static readonly Images Images = new Images(Locale);
		public static readonly Internet Internet = new Internet(Locale);
		public static readonly Lorem Lorem = new Lorem(Locale);
		public static readonly Name Name = new Name(Locale);
		public static readonly PhoneNumbers PhoneNumbers = new PhoneNumbers(Locale);
		public static readonly Rant Rant = new Rant();
		public static readonly Bogus.DataSets.System System = new Bogus.DataSets.System(Locale);


		public static string Parse(string str) => Faker.Parse(str);
        
		public static T PickRandom<T>(IEnumerable<T> items) => Faker.PickRandom(items);
		public static T PickRandom<T>(IList<T> items) => Faker.PickRandom(items);
		public static T PickRandom<T>(ICollection<T> items) => Faker.PickRandom(items);
		public static T PickRandom<T>(List<T> items) => Faker.PickRandom(items);
		public static T PickRandom<T>(T[] items) => Faker.PickRandom(items);
		public static T PickRandomParam<T>(T[] items) => Faker.PickRandomParam(items);
		public static IEnumerable<T> PickRandom<T>(IEnumerable<T> items, int amountToPick) => Faker.PickRandom(items, amountToPick);
		public static IList<T> Make<T>(int count, Func<T> action) => Faker.Make(count, action);
		public static IList<T> Make<T>(int count, Func<int, T> action) => Faker.Make(count, action);
		public static IEnumerable<T> MakeLazy<T>(int count, Func<T> action) => Faker.MakeLazy(count, action);
		public static IEnumerable<T> MakeLazy<T>(int count, Func<int, T> action) => Faker.MakeLazy(count, action);
		public static string ZipCode(string format ="") => Address.ZipCode(format);
		public static string City() => Address.City();
		public static string StreetAddress(System.Boolean useFullAddress) => Address.StreetAddress(useFullAddress);
		public static string CityPrefix() => Address.CityPrefix();
		public static string CitySuffix() => Address.CitySuffix();
		public static string StreetName() => Address.StreetName();
		public static string BuildingNumber() => Address.BuildingNumber();
		public static string StreetSuffix() => Address.StreetSuffix();
		public static string SecondaryAddress() => Address.SecondaryAddress();
		public static string County() =>  Address.County();
		public static string Country() => Address.Country();
		public static string FullAddress() => Address.FullAddress();
		public static string CountryCode(string format) => Address.CountryCode(Extensions.ParseEnum<Iso3166Format>(format));
		public static string State() => Address.State();
		public static string StateAbbr() => Address.StateAbbr();
		public static System.Double Latitude(System.Double min, System.Double max) => Address.Latitude(min, max);
		public static System.Double Longitude(System.Double min, System.Double max) => Address.Longitude(min, max);
		public static string Direction(System.Boolean useAbbreviation) => Address.Direction(useAbbreviation);
		public static string CardinalDirection(System.Boolean useAbbreviation) => Address.CardinalDirection(useAbbreviation);
		public static string OrdinalDirection(System.Boolean useAbbreviation) => Address.OrdinalDirection(useAbbreviation);
		public static string Department(int max, System.Boolean returnMax) => Commerce.Department(max, returnMax);
		public static string Price(decimal min, decimal max, int decimals, string symbol) => Commerce.Price(min, max, decimals, symbol);
		public static System.String[] Categories(int num) => Commerce.Categories(num);
		public static string ProductName() => Commerce.ProductName();
		public static string Color() => Commerce.Color();
		public static string Product() => Commerce.Product();
		public static string ProductAdjective() => Commerce.ProductAdjective();
		public static string ProductMaterial() => Commerce.ProductMaterial();
		public static string CompanySuffix() => Company.CompanySuffix();
		public static string CompanyName(Int32 formatIndex) => Company.CompanyName(formatIndex);
		public static string CompanyName(string format) => Company.CompanyName(format);
		public static string CatchPhrase() => Company.CatchPhrase();
		public static string Bs() => Company.Bs();
		public static string Column() => Database.Column();
		public static string Type() => Database.Type();
		public static string Collation() => Database.Collation();
		public static string Engine() => Database.Engine();
		public static DateTime Past(int yearsToGoBack, DateTime refDate) => Date.Past(yearsToGoBack, refDate);
		public static DateTime Soon(int days) => Date.Soon(days);
		public static DateTime Future(int yearsToGoForward, DateTime refDate) => Date.Future(yearsToGoForward, refDate);
		public static DateTime Between(DateTime start, DateTime end) => Date.Between(start, end);
		public static DateTime Recent(int days) => Date.Recent(days);
		public static TimeSpan Timespan(TimeSpan maxSpan) => Date.Timespan(maxSpan);
		public static string Month(System.Boolean abbrivation, System.Boolean useContext) => Date.Month(abbrivation, useContext);
		public static string Weekday(System.Boolean abbrivation, System.Boolean useContext) => Date.Weekday(abbrivation, useContext);
		public static string Account(int length) => Finance.Account(length);
		public static string AccountName() => Finance.AccountName();
		public static decimal Amount(decimal min, decimal max, int decimals) => Finance.Amount(min, max, decimals);
		public static string TransactionType() => Finance.TransactionType();
		public static Currency Currency(System.Boolean includeFundCodes) => Finance.Currency(includeFundCodes);
		public static string CreditCardNumber() => Finance.CreditCardNumber(null);
		public static string CreditCardCvv() => Finance.CreditCardCvv();
		public static string BitcoinAddress() => Finance.BitcoinAddress();
		public static string EthereumAddress() => Finance.EthereumAddress();
		public static string RoutingNumber() => Finance.RoutingNumber();
		public static string Bic() => Finance.Bic();
		public static string Iban(System.Boolean formatted) => Finance.Iban(formatted);
		public static string Abbreviation() => Hacker.Abbreviation();
		public static string Adjective() => Hacker.Adjective();
		public static string Noun() => Hacker.Noun();
		public static string Verb() => Hacker.Verb();
		public static string IngVerb() => Hacker.IngVerb();
		public static string Phrase() => Hacker.Phrase();
		public static string Image(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Image(width, height, randomize, https);
		public static string Abstract(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Abstract(width, height, randomize, https);
		public static string Animals(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Animals(width, height, randomize, https);
		public static string Business(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Business(width, height, randomize, https);
		public static string Cats(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Cats(width, height, randomize, https);
		public static string City(int width, int height, System.Boolean randomize, System.Boolean https) => Images.City(width, height, randomize, https);
		public static string Food(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Food(width, height, randomize, https);
		public static string Nightlife(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Nightlife(width, height, randomize, https);
		public static string Fashion(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Fashion(width, height, randomize, https);
		public static string People(int width, int height, System.Boolean randomize, System.Boolean https) => Images.People(width, height, randomize, https);
		public static string Nature(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Nature(width, height, randomize, https);
		public static string Sports(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Sports(width, height, randomize, https);
		public static string Technics(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Technics(width, height, randomize, https);
		public static string Transport(int width, int height, System.Boolean randomize, System.Boolean https) => Images.Transport(width, height, randomize, https);
		public static string DataUri(int width, int height) => Images.DataUri(width, height);
		public static string Avatar() => Internet.Avatar();
		public static string Email(string firstName, string lastName, string provider) => Internet.Email(firstName, lastName, provider);
		public static string ExampleEmail(string firstName, string lastName) => Internet.ExampleEmail(firstName, lastName);
		public static string UserName(string firstName, string lastName) => Internet.UserName(firstName, lastName);
		public static string DomainName() => Internet.DomainName();
		public static string DomainWord() => Internet.DomainWord();
		public static string DomainSuffix() => Internet.DomainSuffix();
		public static string Ip() => Internet.Ip();
		public static string Ipv6() => Internet.Ipv6();
		public static string UserAgent() => Internet.UserAgent();
		public static string Mac(string separator) => Internet.Mac(separator);
		public static string Password(int length, System.Boolean memorable, string regexPattern, string prefix) => Internet.Password(length, memorable, regexPattern, prefix);
		public static string Color(System.Byte baseRed, System.Byte baseGreen, System.Byte baseBlue, System.Boolean grayscale, string format) => Internet.Color(baseRed, baseGreen, baseBlue, grayscale,Extensions.ParseEnum<ColorFormat>(format) );
		public static string Protocol() => Internet.Protocol();
		public static string Url() => Internet.Url();
		public static string UrlWithPath(string protocol, string domain) => Internet.UrlWithPath(protocol, domain);
		public static string Word() => Lorem.Word();
		public static System.String[] Words(int num) => Lorem.Words(num);
		public static string Letter(int num) => Lorem.Letter(num);
		public static string Sentence(Int32 wordCount, Int32 range) => Lorem.Sentence(wordCount, range);
		public static string Sentences(Int32 sentenceCount, string separator) => Lorem.Sentences(sentenceCount, separator);
		public static string Paragraph(int min) => Lorem.Paragraph(min);
		public static string Paragraphs(int count, string separator) => Lorem.Paragraphs(count, separator);
		public static string Paragraphs(int min, int max, string separator) => Lorem.Paragraphs(min, max, separator);
		public static string Text() => Lorem.Text();
		public static string Lines(Int32 lineCount, string seperator) => Lorem.Lines(lineCount, seperator);
		public static string Slug(int wordcount) => Lorem.Slug(wordcount);
		public static string FirstName(string gender) => Name.FirstName(Extensions.ParseEnum<Gender>(gender));
		public static string LastName(string gender) => Name.LastName(Extensions.ParseEnum<Gender>(gender));
		public static string FullName(string gender) => Name.FullName(Extensions.ParseEnum<Gender>(gender));
		public static string Prefix(string gender) => Name.Prefix(Extensions.ParseEnum<Gender>(gender));
		public static string FirstName(Gender gender) => Name.FirstName(gender);
		public static string LastName(Gender gender) => Name.LastName(gender);
		public static string FullName(Gender gender) => Name.FullName(gender);
		public static string Prefix(Gender gender) => Name.Prefix(gender);
		public static string Suffix() => Name.Suffix();
		public static string FindName(string firstName, string lastName, Boolean withPrefix, Boolean withSuffix, string gender) => Name.FindName(firstName, lastName, withPrefix, withSuffix, Extensions.ParseEnum<Gender>(gender));
		public static string JobTitle() => Name.JobTitle();
		public static string JobDescriptor() => Name.JobDescriptor();
		public static string JobArea() => Name.JobArea();
		public static string JobType() => Name.JobType();
		public static string PhoneNumber(string format) => PhoneNumbers.PhoneNumber(format);
		public static string PhoneNumberFormat(int phoneFormatsArrayIndex) => PhoneNumbers.PhoneNumberFormat(phoneFormatsArrayIndex);
		public static string Review(string product) => Rant.Review(product);
		public static System.String[] Reviews(string product, int lines) => Rant.Reviews(product, lines);
		public static string FileName(string ext) => System.FileName(ext);
		public static string DirectoryPath() => System.DirectoryPath();
		public static string FilePath() => System.FilePath();
		public static string CommonFileName(string ext) => System.CommonFileName(ext);
		public static string MimeType() => System.MimeType();
		public static string CommonFileType() => System.CommonFileType();
		public static string CommonFileExt() => System.CommonFileExt();
		public static string FileType() => System.FileType();
		public static string FileExt(string mimeType) => System.FileExt(mimeType);
		public static string Semver() => System.Semver();
		public static Version Version() => System.Version();
		public static Exception Exception() => System.Exception();
		public static string AndroidId() => System.AndroidId();
		public static string ApplePushToken() => System.ApplePushToken();
		public static string BlackBerryPin() => System.BlackBerryPin();
	}



}
