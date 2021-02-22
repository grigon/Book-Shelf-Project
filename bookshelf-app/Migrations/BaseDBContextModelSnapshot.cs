﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bookshelf.Context;

namespace bookshelf_app.Migrations
{
    [DbContext(typeof(BaseDBContext))]
    partial class BaseDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("bookshelf.Model.Books.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("bookshelf.Model.Books.BookHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserBookId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("Id");

                    b.HasIndex("UserBookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookHistory");
                });

            modelBuilder.Entity("bookshelf.Model.Books.BookISBN", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("ISBN")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookISBN");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR(60)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<int>("Votes")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("bookshelf.Model.Books.UserBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<bool>("Borrowed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBooks");
                });

            modelBuilder.Entity("bookshelf.Model.Chats.Chat", b =>
                {
                    b.Property<Guid>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("bookshelf.Model.Chats.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("ChatId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MessageAuthorId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("MessageAuthorId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("bookshelf.Model.Chats.ChatUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("ChatId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatUsers");
                });

            modelBuilder.Entity("bookshelf.Model.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR(60)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("bookshelf.Model.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("City")
                        .HasColumnType("VARCHAR(70)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("VARCHAR(72)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR(60)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("bookshelf.Model.Users.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Book", b =>
                {
                    b.HasOne("bookshelf.Model.Books.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("bookshelf.Model.Books.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("bookshelf.Model.Books.BookHistory", b =>
                {
                    b.HasOne("bookshelf.Model.Books.UserBook", "UserBook")
                        .WithMany()
                        .HasForeignKey("UserBookId");

                    b.HasOne("bookshelf.Model.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");

                    b.Navigation("UserBook");
                });

            modelBuilder.Entity("bookshelf.Model.Books.BookISBN", b =>
                {
                    b.HasOne("bookshelf.Model.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Review", b =>
                {
                    b.HasOne("bookshelf.Model.Books.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId");

                    b.HasOne("bookshelf.Model.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("bookshelf.Model.Books.UserBook", b =>
                {
                    b.HasOne("bookshelf.Model.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("bookshelf.Model.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("bookshelf.Model.Chats.ChatMessage", b =>
                {
                    b.HasOne("bookshelf.Model.Chats.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId");

                    b.HasOne("bookshelf.Model.Users.User", "MessageAuthor")
                        .WithMany()
                        .HasForeignKey("MessageAuthorId");

                    b.Navigation("Chat");

                    b.Navigation("MessageAuthor");
                });

            modelBuilder.Entity("bookshelf.Model.Chats.ChatUser", b =>
                {
                    b.HasOne("bookshelf.Model.Chats.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId");

                    b.HasOne("bookshelf.Model.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("bookshelf.Model.Users.UserRole", b =>
                {
                    b.HasOne("bookshelf.Model.Users.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("bookshelf.Model.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("bookshelf.Model.Books.Book", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
