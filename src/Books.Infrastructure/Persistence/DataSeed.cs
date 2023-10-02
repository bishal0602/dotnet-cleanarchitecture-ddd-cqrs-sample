using Books.Application.Contracts.Services;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Books.Infrastructure.Persistence
{
    public static class DataSeed
    {
        public static void Seed(ModelBuilder modelBuilder, IPasswordHasher<User> passwordHasher, IDateTimeProvider dateTimeProvider)
        {
            var alincoln = User.CreateNew("Abraham", "Lincoln", "alincoln", "lincoln.abraham@example.com", "HonestAbe1865");
            var jcaesar = User.CreateNew("Julius", "Caesar", "jcaesar", "caesar.julius@example.com", "EtTuBrute44BC");
            var aeinstein = User.CreateNew("Albert", "Einstein", "aeinstein", "einstein.albert@example.com", "E=mc2Genius");
            var mcurie = User.CreateNew("Marie", "Curie", "mcurie", "curie.marie@example.com", "Radioactive1898");
            var ldavinci = User.CreateNew("Leonardo", "da Vinci", "ldavinci", "davinci.leonardo@example.com", "Renaissance1452");
            var wshakespeare = User.CreateNew("William", "Shakespeare", "wshakespeare", "shakespeare.william@example.com", "ToBeOrNotToBe1600");
            var ccleopatra = User.CreateNew("Cleopatra", "", "ccleopatra", "cleopatra@example.com", "QueenOfEgypt30BC");
            var aalexander = User.CreateNew("Alexander", "the Great", "aalexander", "alexander@example.com", "Conqueror356BC"); ;
            var ntesla = User.CreateNew("Nikola", "Tesla", "ntesla", "tesla.nikola@example.com", "ACPowerGenius1856");
            var wgenghis = User.CreateNew("Genghis", "Khan", "wgenghis", "genghis.khan@example.com", "MongolEmpire1162");

            var gameOfThrones1 = Book.CreateNew("A Game of Thrones", "The first book in the series, introducing the world of Westeros and its political intrigues.");
            var gameOfThrones2 = Book.CreateNew("A Clash of Kings", "Continues the story as multiple claimants vie for the Iron Throne.");
            var gameOfThrones3 = Book.CreateNew("A Storm of Swords", "The power struggles intensify as war engulfs the Seven Kingdoms.");
            var gameOfThrones4 = Book.CreateNew("A Feast for Crows", "Explores the aftermath of the previous conflicts and the shifting power dynamics.");
            var gameOfThrones5 = Book.CreateNew("A Dance with Dragons", "Continues the storylines of various characters as conflicts escalate.");
            var lotr = Book.CreateNew("The Lord of the Rings", "An epic fantasy trilogy set in the fictional world of Middle-earth.");
            var hobbit = Book.CreateNew("The Hobbit", "A fantasy novel that serves as a prequel to The Lord of the Rings.");
            var silmarillion = Book.CreateNew("The Silmarillion", "A collection of mythopoeic works exploring the mythology of Middle-earth.");
            var hpottersorcerer = Book.CreateNew("Harry Potter and the Sorcerer's Stone", "The first book in the popular Harry Potter series.");
            var fbeasts = Book.CreateNew("Fantastic Beasts and Where to Find Them", "A companion book to the Harry Potter series, featuring magical creatures.");
            var crimenpunishment = Book.CreateNew("Crime and Punishment", "A psychological novel exploring the moral and psychological struggles of its main character.");
            var mockingbird = Book.CreateNew("To Kill a Mockingbird", "A gripping novel set in the Deep South during the era of racial inequality.");
            var seeawatchaman = Book.CreateNew("Go Set a Watchman", "A novel set in the 1950s and considered a sequel to To Kill a Mockingbird.");
            var literally1984 = Book.CreateNew("1984", "A dystopian novel that explores the dangers of totalitarianism.");
            var prideandprejudice = Book.CreateNew("Pride and Prejudice", "A classic romantic novel set in 19th-century England.");
            var greatgatsby = Book.CreateNew("The Great Gatsby", "A story of wealth, love, and the American Dream during the Roaring Twenties.");


            var gMartin = Author.CreateNew("George R.R.", "Martin", "George Raymond Richard Martin, commonly known as George R.R. Martin, is an American novelist and short story writer. He is best known for his series of epic fantasy novels, 'A Song of Ice and Fire', which has been adapted into the television series 'Game of Thrones'. Martin's writing is known for its complex characters, intricate plotlines, and realistic portrayal of political and social dynamics. His work has garnered critical acclaim and has a vast and dedicated fanbase.");
            var jrTolkien = Author.CreateNew("J.R.R.", "Tolkien", "An English writer, poet, and philologist.");
            var cTolkien = Author.CreateNew("Christopher", "Tolkien", "An English writer and the son of J.R.R. Tolkien.");
            var jRowling = Author.CreateNew("J.K.", "Rowling", "A British author best known for the Harry Potter series.");
            var fDostoevsky = Author.CreateNew("Fyodor", "Dostoevsky", "A Russian novelist and philosopher.");
            var hLee = Author.CreateNew("Harper", "Lee", "An American novelist best known for her acclaimed work.");
            var gOrwell = Author.CreateNew("George", "Orwell", "An English author known for his political and social commentary.");
            var jAusten = Author.CreateNew("Jane", "Austen", "An English novelist known for her witty and insightful writing.");
            var fFitzgerald = Author.CreateNew("F. Scott", "Fitzgerald", "An American writer associated with the Jazz Age.");


            var users = new[] { alincoln, jcaesar, aeinstein, mcurie, ldavinci, wshakespeare, ccleopatra, aalexander, ntesla, wgenghis };
            foreach (var user in users)
            {
                var hashedPassword = passwordHasher.HashPassword(user, user.Password);
                user.UpdatePassword(hashedPassword);
            }

            var books = new[] { gameOfThrones1, gameOfThrones2, gameOfThrones3, gameOfThrones4, gameOfThrones5, lotr, hobbit, silmarillion, hpottersorcerer, fbeasts, crimenpunishment, mockingbird, seeawatchaman, literally1984, prideandprejudice, greatgatsby };

            var random = new Random();
            foreach (var book in books)
            {
                DateTime startDate = new DateTime(2000, 1, 1);
                DateTime endDate = dateTimeProvider.Now;
                TimeSpan timeSpan = endDate - startDate;
                TimeSpan randomSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
                book.CreatedOn = startDate + randomSpan;

                var randomUserIndex = random.Next(0, users.Length);
                var randomUser = users[randomUserIndex];
                book.CreatedByUserId = randomUser.Id;
            }

            var authors = new[] { gMartin, jrTolkien, cTolkien, jRowling, fDostoevsky, hLee, gOrwell, jAusten, fFitzgerald };

            var authorBooks = new[]
        {
            AuthorBook.CreateNew(gMartin.Id, gameOfThrones1.Id),
            AuthorBook.CreateNew(gMartin.Id, gameOfThrones2.Id),
            AuthorBook.CreateNew(gMartin.Id, gameOfThrones3.Id),
            AuthorBook.CreateNew(gMartin.Id, gameOfThrones4.Id),
            AuthorBook.CreateNew(gMartin.Id, gameOfThrones5.Id),
            AuthorBook.CreateNew(jrTolkien.Id, lotr.Id),
            AuthorBook.CreateNew(cTolkien.Id, lotr.Id),
            AuthorBook.CreateNew(jrTolkien.Id, hobbit.Id),
            AuthorBook.CreateNew(jrTolkien.Id, silmarillion.Id),
            AuthorBook.CreateNew(jRowling.Id, hpottersorcerer.Id),
            AuthorBook.CreateNew(jRowling.Id, fbeasts.Id),
            AuthorBook.CreateNew(fDostoevsky.Id, crimenpunishment.Id),
            AuthorBook.CreateNew(hLee.Id, mockingbird.Id),
            AuthorBook.CreateNew(hLee.Id, seeawatchaman.Id),
            AuthorBook.CreateNew(gOrwell.Id, literally1984.Id),
            AuthorBook.CreateNew(jAusten.Id, prideandprejudice.Id),
            AuthorBook.CreateNew(fFitzgerald.Id, greatgatsby.Id),
        };

            var bookReviews = new[]
            {
                BookReview.CreateNew(alincoln.UserName, "Great book, highly recommended!", gameOfThrones1.Id),
                BookReview.CreateNew(jcaesar.UserName, "I couldn't put it down, fantastic read!", gameOfThrones1.Id),
                BookReview.CreateNew(aeinstein.UserName, "An epic tale with intricate plot twists.", gameOfThrones2.Id),
                BookReview.CreateNew(mcurie.UserName, "Captivating and immersive, loved every page.", gameOfThrones2.Id),
                BookReview.CreateNew(ldavinci.UserName, "A masterpiece of fantasy literature.", lotr.Id),
                BookReview.CreateNew(wshakespeare.UserName, "Beautifully written, a timeless classic.", hobbit.Id),
                BookReview.CreateNew(ccleopatra.UserName, "Intriguing characters and rich storytelling.", silmarillion.Id),
                BookReview.CreateNew(aalexander.UserName, "A fascinating journey through Middle-earth.", silmarillion.Id),
                BookReview.CreateNew(ntesla.UserName, "Magical and enchanting, a must-read for all ages.", hpottersorcerer.Id),
                BookReview.CreateNew(wgenghis.UserName, "Thought-provoking and profound.", hpottersorcerer.Id),
                BookReview.CreateNew(alincoln.UserName, "A gripping and powerful novel.", crimenpunishment.Id),
                BookReview.CreateNew(jcaesar.UserName, "This book changed my perspective on life.", crimenpunishment.Id),
                BookReview.CreateNew(aeinstein.UserName, "A classic that stands the test of time.", mockingbird.Id),
                BookReview.CreateNew(mcurie.UserName, "A remarkable and poignant story.", mockingbird.Id),
                BookReview.CreateNew(ldavinci.UserName, "An unsettling and thought-provoking dystopian tale.", literally1984.Id),
                BookReview.CreateNew(wshakespeare.UserName, "A delightful and engaging romance.", prideandprejudice.Id),
                BookReview.CreateNew(ccleopatra.UserName, "An iconic representation of the Jazz Age.", greatgatsby.Id),
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<AuthorBook>().HasData(authorBooks);
            modelBuilder.Entity<BookReview>().HasData(bookReviews);

        }
    }
}
