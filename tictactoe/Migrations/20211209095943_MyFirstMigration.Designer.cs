﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tictactoe.Data.Database;

namespace tictactoe.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211209095943_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("tictactoe.Data.Entities.Game", b =>
                {
                    b.Property<int>("Game_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("gameStatusStatus_Id")
                        .HasColumnType("int");

                    b.HasKey("Game_Id");

                    b.HasIndex("gameStatusStatus_Id");

                    b.ToTable("games");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.GameStatus", b =>
                {
                    b.Property<int>("Status_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<string>("Outcome")
                        .HasColumnType("text");

                    b.HasKey("Status_Id");

                    b.ToTable("gameStatuses");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.PlaceToken", b =>
                {
                    b.Property<int>("token_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("col")
                        .HasColumnType("int");

                    b.Property<int>("game_id")
                        .HasColumnType("int");

                    b.Property<int>("player_id")
                        .HasColumnType("int");

                    b.Property<int>("row")
                        .HasColumnType("int");

                    b.HasKey("token_id");

                    b.ToTable("placeTokens");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.Player", b =>
                {
                    b.Property<int>("Player_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GameStatusStatus_Id")
                        .HasColumnType("int");

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Game_Id1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("token_id")
                        .HasColumnType("int");

                    b.HasKey("Player_Id");

                    b.HasIndex("GameStatusStatus_Id");

                    b.HasIndex("Game_Id1");

                    b.HasIndex("token_id");

                    b.ToTable("players");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.Winner", b =>
                {
                    b.Property<int>("Winner_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GameStatusStatus_Id")
                        .HasColumnType("int");

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<int>("Player_Id")
                        .HasColumnType("int");

                    b.HasKey("Winner_Id");

                    b.HasIndex("GameStatusStatus_Id");

                    b.ToTable("winners");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.Game", b =>
                {
                    b.HasOne("tictactoe.Data.Entities.GameStatus", "gameStatus")
                        .WithMany()
                        .HasForeignKey("gameStatusStatus_Id");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.Player", b =>
                {
                    b.HasOne("tictactoe.Data.Entities.GameStatus", null)
                        .WithMany("players")
                        .HasForeignKey("GameStatusStatus_Id");

                    b.HasOne("tictactoe.Data.Entities.Game", null)
                        .WithMany("Players")
                        .HasForeignKey("Game_Id1");

                    b.HasOne("tictactoe.Data.Entities.PlaceToken", "Token")
                        .WithMany()
                        .HasForeignKey("token_id");
                });

            modelBuilder.Entity("tictactoe.Data.Entities.Winner", b =>
                {
                    b.HasOne("tictactoe.Data.Entities.GameStatus", null)
                        .WithMany("Winners")
                        .HasForeignKey("GameStatusStatus_Id");
                });
#pragma warning restore 612, 618
        }
    }
}