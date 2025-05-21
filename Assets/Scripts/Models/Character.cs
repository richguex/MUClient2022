﻿namespace UniMU.Models
{
    using System;
    using System.Collections.Generic;
    using MUnique.OpenMU.AttributeSystem;
    using MUnique.OpenMU.DataModel.Configuration;
    using MUnique.OpenMU.DataModel.Entities;
    using MUnique.OpenMU.Interfaces;

    /// <summary>
    /// The hero state of a player. Given enough time, the state converges to <see cref="Normal"/>.
    /// </summary>
    public enum HeroState
    {
        /// <summary>
        /// The character is new.
        /// </summary>
        New,

        /// <summary>
        /// The character is a hero.
        /// </summary>
        Hero,

        /// <summary>
        /// The character is a hero, but it's some time ago.
        /// </summary>
        MediumHero,

        /// <summary>
        /// The character is a hero, but the hero state is almost gone.
        /// </summary>
        LightHero,

        /// <summary>
        /// The normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// The character killed another character, and has a kill warning.
        /// </summary>
        PlayerKillWarning,

        /// <summary>
        /// The character killed two characters, and has some restrictions.
        /// </summary>
        PlayerKiller1stStage,

        /// <summary>
        /// The character killed more than two characters, and has hard restrictions.
        /// </summary>
        PlayerKiller2ndStage
    }

    /// <summary>
    /// The character of a player.
    /// </summary>
    public class Character
    {
        public const ushort ConstantPlayerId = 0x0102;

        public ushort Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the character class.
        /// </summary>
        public CharacterClass CharacterClass { get; set; }

        /// <summary>
        /// Gets or sets the character slot in the account.
        /// </summary>
        public byte CharacterSlot { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        public long Experience { get; set; }

        public long NextLevelExperience { get; set; }

        public AttributeSystem Attributes { get; set; } = new AttributeSystem(new List<IAttribute>(),
                                                                              new List<IAttribute>(),
                                                                              new List<AttributeRelationship>());
        /// <summary>
        /// Gets or sets the master experience.
        /// </summary>
        public long MasterExperience { get; set; }

        /// <summary>
        /// Gets or sets the remaining level up points which can be spent on increasable stat attributes.
        /// </summary>
        public int LevelUpPoints { get; set; }

        /// <summary>
        /// Gets or sets the master level up points which can be spent on master skills.
        /// </summary>
        public int MasterLevelUpPoints { get; set; }

        public int Money { get; set; }
        /// <summary>
        /// Gets or sets the current game map.
        /// </summary>
        public GameMapDefinition CurrentMap { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate of its map position.
        /// </summary>
        public byte PositionX { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of its map position.
        /// </summary>
        public byte PositionY { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate of its map position.
        /// </summary>
        public byte LastPositionX { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of its map position.
        /// </summary>
        public byte LastPositionY { get; set; }

        /// <summary>
        /// Gets or sets the player kill count.
        /// </summary>
        public int PlayerKillCount { get; set; }

        /// <summary>
        /// Gets or sets the remaining seconds for the current hero state, when the player state is not normal.
        /// </summary>
        public int StateRemainingSeconds { get; set; }

        /// <summary>
        /// Gets or sets the hero state.
        /// </summary>
        public HeroState State { get; set; }

        /// <summary>
        /// Gets or sets the quest info. Don't know yet what its content is.
        /// </summary>
        public byte[] QuestInfo { get; set; }

        /// <summary>
        /// Gets or sets the used fruit points.
        /// </summary>
        public int UsedFruitPoints { get; set; }

        /// <summary>
        /// Gets or sets the used negative fruit points.
        /// </summary>
        public int UsedNegFruitPoints { get; set; }

        /// <summary>
        /// Gets or sets the number of inventory extensions.
        /// </summary>
        public int InventoryExtensions { get; set; }

        /// <summary>
        /// Gets or sets the stat attributes.
        /// </summary>
        public int Level { get; set; }


        /// <summary>
        /// Gets or sets the learned skills.
        /// </summary>
        public ICollection<SkillEntry> LearnedSkills { get; protected set; }

        /// <summary>
        /// Gets or sets the inventory.
        /// </summary>
        public ItemStorage Inventory { get; set; }

        /// <summary>
        /// Gets or sets the drop item groups.
        /// </summary>
        public ICollection<DropItemGroup> DropItemGroups { get; protected set; }

        public override string ToString()
        {
            return $"slot:{CharacterSlot}, id:{Id} name:{Name} lvl:{Level} x:{PositionX} y:{PositionY}";
        }
    }
}