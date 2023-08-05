/*
 Navicat MySQL Data Transfer

 Source Server         : Muhammad
 Source Server Type    : MySQL
 Source Server Version : 50717
 Source Host           : localhost:3306
 Source Schema         : acc_server

 Target Server Type    : MySQL
 Target Server Version : 50717
 File Encoding         : 65001

 Date: 10/12/2018 13:05:24
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Username` char(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Password` char(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `IP` char(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `LastCheck` bigint(255) UNSIGNED NULL DEFAULT 0,
  `State` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `EntityID` bigint(18) UNSIGNED NULL DEFAULT 0,
  `Email` char(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `Question` char(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `answer` char(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Country` char(110) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `City` char(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `secretquestion` char(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `realname` char(25) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `machine` char(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `lastvote` char(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `mobilenumber` bigint(18) NULL DEFAULT 0,
  `securitycode` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `date` varchar(0) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `joined` varchar(220) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Online` tinyint(2) NULL DEFAULT NULL,
  `UID` bigint(18) NULL DEFAULT NULL,
  `Class` tinyint(5) NULL DEFAULT NULL,
  PRIMARY KEY (`Username`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for arena
-- ----------------------------
DROP TABLE IF EXISTS `arena`;
CREATE TABLE `arena`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EntityName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `ArenaPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `ActivityPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayBattles` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalLose` int(10) UNSIGNED NULL DEFAULT 0,
  `CurrentHonor` int(10) UNSIGNED NULL DEFAULT 0,
  `HistoryHonor` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonRank` int(10) UNSIGNED NULL DEFAULT 0,
  `Level` int(10) UNSIGNED NULL DEFAULT 0,
  `Class` int(10) UNSIGNED NULL DEFAULT 0,
  `ArenaPointFill` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Model` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonArenaPoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonWin` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonLose` int(10) UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`EntityID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`EntityID`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of arena
-- ----------------------------
INSERT INTO `arena` VALUES (0, 'nmnmmn', 1000, 0, 0, 0, 0, 0, 0, 0, 0, 34, 161, 636589877529316781, 281003, 0, 0, 0);

-- ----------------------------
-- Table structure for clans
-- ----------------------------
DROP TABLE IF EXISTS `clans`;
CREATE TABLE `clans`  (
  `Identifier` int(32) NOT NULL DEFAULT 0,
  `LeaderId` int(32) NOT NULL DEFAULT 0,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `Fund` int(32) NOT NULL DEFAULT 0,
  `Announcement` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'This is new clan!',
  `BPTower` int(32) NOT NULL DEFAULT 0,
  `Level` int(32) NOT NULL DEFAULT 0,
  `LeaderName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `polekeeper` int(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Identifier`) USING BTREE,
  UNIQUE INDEX `myIndex`(`Identifier`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for configuration
-- ----------------------------
DROP TABLE IF EXISTS `configuration`;
CREATE TABLE `configuration`  (
  `EntityID` bigint(10) UNSIGNED NULL DEFAULT 1000000,
  `Server` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Server`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of configuration
-- ----------------------------
INSERT INTO `configuration` VALUES (1000000, 'Dark');
INSERT INTO `configuration` VALUES (1000000, 'Light');

-- ----------------------------
-- Table structure for enemy
-- ----------------------------
DROP TABLE IF EXISTS `enemy`;
CREATE TABLE `enemy`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EnemyID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EnemyName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT ''
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for entities
-- ----------------------------
DROP TABLE IF EXISTS `entities`;
CREATE TABLE `entities`  (
  `UID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `Owner` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `HairStyle` smallint(12) UNSIGNED NULL DEFAULT 430,
  `Class` tinyint(5) UNSIGNED NOT NULL DEFAULT 10,
  `Money` bigint(18) UNSIGNED NULL DEFAULT 50000,
  `ConquerPointsFake` bigint(18) NOT NULL DEFAULT 0,
  `ConquerPoints` bigint(18) UNSIGNED NULL DEFAULT 0,
  `FirstCreditPoints` bigint(18) NOT NULL DEFAULT 0,
  `Body` smallint(12) UNSIGNED NOT NULL DEFAULT 0,
  `Face` smallint(12) UNSIGNED NOT NULL DEFAULT 0,
  `Level` tinyint(5) UNSIGNED NULL DEFAULT 1,
  `Strength` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Agility` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Vitality` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Spirit` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Atributes` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Hitpoints` mediumint(16) UNSIGNED NULL DEFAULT 93,
  `Mana` mediumint(16) UNSIGNED NULL DEFAULT 0,
  `MapID` smallint(12) UNSIGNED NULL DEFAULT 1002,
  `X` smallint(12) UNSIGNED NULL DEFAULT 298,
  `Y` smallint(12) UNSIGNED NULL DEFAULT 270,
  `PKPoints` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Experience` bigint(255) UNSIGNED NULL DEFAULT 0,
  `QuizPoints` mediumint(30) UNSIGNED NULL DEFAULT 0,
  `PreviousMapID` smallint(12) UNSIGNED NULL DEFAULT 1010,
  `Reborn` tinyint(2) UNSIGNED NULL DEFAULT 0,
  `1stClass` tinyint(5) UNSIGNED NULL DEFAULT 10,
  `2ndClass` tinyint(2) UNSIGNED NULL DEFAULT 0,
  `3rdClass` tinyint(2) UNSIGNED NULL DEFAULT 0,
  `Spouse` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT 'None',
  `WarehousePW` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `MoneySave` bigint(18) UNSIGNED NULL DEFAULT 0,
  `FirstRebornClass` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `SecondRebornClass` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `FirstRebornLevel` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `SecondRebornLevel` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `Online` tinyint(2) UNSIGNED NULL DEFAULT 0,
  `EnlightenPoints` bigint(18) UNSIGNED NULL DEFAULT 0,
  `DoubleExpTime` bigint(18) UNSIGNED NULL DEFAULT 0,
  `gwkill` bigint(18) UNSIGNED NULL DEFAULT 0,
  `HeavenBlessingTime` bigint(255) UNSIGNED NULL DEFAULT 0,
  `BlessTime` bigint(18) UNSIGNED NULL DEFAULT 0,
  `LastDragonBallUse` bigint(255) NULL DEFAULT 0,
  `LastResetTime` bigint(255) NULL DEFAULT 0,
  `EnlightsReceived` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `EnlightmentWait` mediumint(100) UNSIGNED NULL DEFAULT 0,
  `DoubleExpToday` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `GuildID` bigint(18) UNSIGNED NULL DEFAULT 0,
  `GuildRank` bigint(18) UNSIGNED NULL DEFAULT 0,
  `GuildSilverDonation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `GuildConquerPointDonation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `VIPLevel` tinyint(5) UNSIGNED NOT NULL,
  `VirtuePoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `HeadgearClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `NecklaceClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `ArmorClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `WeaponClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `BootsClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `TowerClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `FanClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `RingClaim` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `VendingDisguise` smallint(5) NULL DEFAULT 0,
  `ChatBanTime` bigint(255) NULL DEFAULT 0,
  `ChatBanLasts` bigint(255) NULL DEFAULT 0,
  `ChatBanned` tinyint(5) NULL DEFAULT 0,
  `InLottery` tinyint(5) NULL DEFAULT 0,
  `LotteryEntries` mediumint(10) NULL DEFAULT 0,
  `LastLotteryEntry` bigint(255) NULL DEFAULT 0,
  `PreviousX` mediumint(10) NULL DEFAULT 0,
  `PreviousY` mediumint(10) NULL DEFAULT 0,
  `OfflineTGEnterTime` bigint(255) NULL DEFAULT 0,
  `ExpBalls` mediumint(10) NULL DEFAULT 0,
  `Status` bigint(18) NULL DEFAULT 0,
  `Status2` bigint(18) NULL DEFAULT 0,
  `UnionID` bigint(18) UNSIGNED NULL DEFAULT 0,
  `ClanId` int(36) UNSIGNED NULL DEFAULT 0,
  `ClanDonation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `ClanRank` int(36) UNSIGNED NULL DEFAULT 0,
  `SubPro` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `SubProLevel` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `StudyPoints` int(36) UNSIGNED NULL DEFAULT 0,
  `LastLogin` bigint(16) NULL DEFAULT 0,
  `My_Title` int(36) NOT NULL DEFAULT 0,
  `Status3` bigint(32) NOT NULL DEFAULT 0,
  `Status4` bigint(32) NOT NULL DEFAULT 0,
  `Quest` bigint(32) NOT NULL DEFAULT 0,
  `Flower` bigint(32) NOT NULL DEFAULT 0,
  `namechange` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CountryID` smallint(4) NULL DEFAULT NULL,
  `spiritquestbead` int(4) NOT NULL DEFAULT 0,
  `canacceptspiritbead` int(1) NOT NULL DEFAULT 0,
  `collectedspirits` int(4) NOT NULL DEFAULT 0,
  `EditCount` smallint(12) NULL DEFAULT 0,
  `EditAllowed` smallint(12) NOT NULL DEFAULT 1,
  `claimed` tinyint(2) NOT NULL DEFAULT 0,
  `RacePoints` bigint(18) NOT NULL DEFAULT 0,
  `TotalDoantionGuild` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `ArsenalDonation` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `OnlinePoints` bigint(18) NOT NULL DEFAULT 0,
  `BoundCps` bigint(18) UNSIGNED NULL DEFAULT 300,
  `LuckyPoints` int(4) NOT NULL DEFAULT 0,
  `updatelist` bigint(32) NULL DEFAULT NULL,
  `Exploits` bigint(255) NULL DEFAULT NULL,
  `ClaimedTeamPK` tinyint(2) NOT NULL,
  `ClaimedSTeamPK` tinyint(2) NOT NULL,
  `GuildLilies` bigint(255) NULL DEFAULT NULL,
  `GuildRouses` bigint(255) NULL DEFAULT NULL,
  `GuildOrchids` bigint(255) NULL DEFAULT NULL,
  `GuildTulips` bigint(255) NULL DEFAULT NULL,
  `GuildPkDonation` bigint(255) NULL DEFAULT NULL,
  `CTFCpsReward` bigint(255) NULL DEFAULT NULL,
  `CTFSilverReward` bigint(255) NULL DEFAULT NULL,
  `joinkingtime` bigint(255) NULL DEFAULT NULL,
  `Kingfight` bigint(255) NULL DEFAULT NULL,
  `kingwin` bigint(255) NULL DEFAULT NULL,
  `TQPoint` bigint(255) NULL DEFAULT NULL,
  `OnlineTrainning` bigint(18) NULL DEFAULT NULL,
  `HuntingExp` bigint(18) NULL DEFAULT NULL,
  `ForgetPassword` bigint(32) NULL DEFAULT NULL,
  `VoteSystem` bigint(32) NULL DEFAULT NULL,
  `FirstCredit` tinyint(2) NOT NULL,
  `ClaimedElitePk` tinyint(2) NOT NULL,
  `CursedTime` bigint(255) NOT NULL,
  `SwordSoul` bigint(32) NOT NULL,
  `BansheeSpirit` bigint(32) NOT NULL,
  `ProtectionTime` bigint(18) NULL DEFAULT NULL,
  `ClaimedExp` bigint(32) NOT NULL,
  `Energy` bigint(255) NULL DEFAULT NULL,
  `ExtraInventory` bigint(255) NULL DEFAULT NULL,
  `Appearance` tinyint(5) NOT NULL,
  `JiangSettings` tinyint(5) NOT NULL,
  `Name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `strikePoints` bigint(16) NULL DEFAULT NULL,
  `lacb` bigint(16) NULL DEFAULT NULL,
  `TitlePoints` bigint(20) NULL DEFAULT 0,
  `ChampionPoints` bigint(16) NOT NULL DEFAULT 0,
  `Offical` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Harem` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Guards` smallint(12) UNSIGNED NULL DEFAULT 0,
  `UnionExploits` bigint(18) UNSIGNED NULL DEFAULT 0,
  `TempestWings` int(12) NOT NULL,
  `InnerBook` bigint(18) NOT NULL DEFAULT 0,
  `ProsbertyPack` bigint(18) NOT NULL DEFAULT 0,
  `Champketos` bigint(18) NOT NULL DEFAULT 0,
  `EpicTimes` bigint(18) NULL DEFAULT 5,
  `NemisisPoint` bigint(18) NULL DEFAULT 0,
  `EpicMonkPrize` bigint(18) NULL DEFAULT 27,
  `EpicMonkTimes` bigint(18) NULL DEFAULT 1,
  `TrojanEpicOn` tinyint(5) NULL DEFAULT NULL,
  `Epictrue` tinyint(5) NULL DEFAULT NULL,
  `FirstStagetrojan` bigint(18) NULL DEFAULT 0,
  `Stars` bigint(18) NULL DEFAULT 0,
  `ewing` bigint(18) NULL DEFAULT 0,
  `etitle` bigint(18) NULL DEFAULT 0,
  `Windwalker` bigint(18) NOT NULL DEFAULT 0,
  `Wardrobe` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `BlackList` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DailySignedDays` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DailySignRewards` bigint(255) NOT NULL DEFAULT 0,
  `DailySignVIPChances` bigint(255) NOT NULL DEFAULT 10,
  `TodayChangedBranch` bigint(18) NOT NULL DEFAULT 0,
  `Prestige` bigint(255) NOT NULL DEFAULT 0,
  `Kingdomquiz` int(40) NOT NULL,
  `DivineRXP` bigint(18) NOT NULL DEFAULT 0,
  `VIPLevelFake` bigint(18) NULL DEFAULT 0,
  `VipStamp` bigint(255) NULL DEFAULT 0,
  `VIPLevelParment` bigint(18) NOT NULL DEFAULT 0,
  `DidBrightFortune` bigint(255) NOT NULL DEFAULT 0,
  `DonefurtuneLastStage` bigint(255) NOT NULL DEFAULT 0,
  `Donefurtunefirststagege` bigint(255) NOT NULL DEFAULT 0,
  `TransferedMessage` bigint(20) NOT NULL DEFAULT 0,
  `winnerctf` bigint(255) NOT NULL DEFAULT 0,
  `s` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `DidFirstHeavenTreasury` int(40) NULL DEFAULT 0,
  `MystreyFruit` smallint(12) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`, `claimed`, `Name`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for eventtime
-- ----------------------------
DROP TABLE IF EXISTS `eventtime`;
CREATE TABLE `eventtime`  (
  `LastmanStart` tinyint(5) UNSIGNED NOT NULL DEFAULT 0,
  `LastmanEnd` tinyint(5) UNSIGNED NOT NULL DEFAULT 0,
  `SSFBStart` tinyint(5) NOT NULL DEFAULT 0,
  `DeathWarStart` tinyint(5) NOT NULL DEFAULT 0,
  `HeroOFGameHour` tinyint(5) NOT NULL DEFAULT 0,
  `HeroOFGameMinute` tinyint(5) NOT NULL,
  `SSFBEnd` tinyint(5) NOT NULL,
  `DeathWarEnd` tinyint(5) NOT NULL,
  `SkyFightStart` tinyint(5) NOT NULL,
  `SkyFightEnd` tinyint(5) NOT NULL,
  `TreasureBoxStart` tinyint(5) NOT NULL,
  `TreasureBoxEnd` tinyint(5) NOT NULL,
  `TwinCityWarHour` tinyint(5) NOT NULL,
  `TwinCityWarMinute` tinyint(5) NOT NULL,
  `ChaseStart` tinyint(5) NOT NULL,
  `ChaseEnd` tinyint(5) NOT NULL
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Table structure for guilds
-- ----------------------------
DROP TABLE IF EXISTS `guilds`;
CREATE TABLE `guilds`  (
  `Name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `ID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Bulletin` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT 'This is a new guild.',
  `SilverFund` bigint(255) UNSIGNED NULL DEFAULT 500000,
  `ConquerPointFund` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Wins` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Losts` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LeaderName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '0',
  `LevelRequirement` int(4) UNSIGNED NULL DEFAULT 1,
  `RebornRequirement` int(4) UNSIGNED NULL DEFAULT 0,
  `ClassRequirement` int(4) UNSIGNED NULL DEFAULT 0,
  `CTFPoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `CTFReward` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Guild_Advertise` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `Advertise` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `GuildEnRole` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BuletinEnRole` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CTFDonationCps` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CTFDonationSilver` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CTFdonationSilverold` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CTFdonationCpsold` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AnnouncementDate` bigint(16) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inbox
-- ----------------------------
DROP TABLE IF EXISTS `inbox`;
CREATE TABLE `inbox`  (
  `Username` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Email` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Message` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Time` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for macs
-- ----------------------------
DROP TABLE IF EXISTS `macs`;
CREATE TABLE `macs`  (
  `mac` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `id` bigint(20) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for online
-- ----------------------------
DROP TABLE IF EXISTS `online`;
CREATE TABLE `online`  (
  `Name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `OnlineCount` int(11) NULL DEFAULT 0,
  PRIMARY KEY (`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of online
-- ----------------------------
INSERT INTO `online` VALUES ('Dark', 0);
INSERT INTO `online` VALUES ('Light', 0);

-- ----------------------------
-- Table structure for servers
-- ----------------------------
DROP TABLE IF EXISTS `servers`;
CREATE TABLE `servers`  (
  `Name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `IP` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Port` int(16) UNSIGNED NULL DEFAULT NULL,
  `TransferKey` varchar(64) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  `TransferSalt` varchar(64) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  PRIMARY KEY (`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of servers
-- ----------------------------
INSERT INTO `servers` VALUES ('Dark', '192.168.1.2', 5836, 'EypKhLvYJ3zdLCTyz9Ak8RAgM78tY5F32b7CUXDuLDJDFBH8H67BWy9QThmaN5VS', 'MyqVgBf3ytALHWLXbJxSUX4uFEu3Xmz2UAY9sTTm8AScB7Kk2uwqDSnuNJske4BJ');
INSERT INTO `servers` VALUES ('Light', '192.168.1.2', 5837, 'EypKhLvYJ3zdLCTyz9Ak8RAgM78tY5F32b7CUXDuLDJDFBH8H67BWy9QThmaN5VS', 'MyqVgBf3ytALHWLXbJxSUX4uFEu3Xmz2UAY9sTTm8AScB7Kk2uwqDSnuNJske4BJ');

-- ----------------------------
-- Table structure for votes_accounts
-- ----------------------------
DROP TABLE IF EXISTS `votes_accounts`;
CREATE TABLE `votes_accounts`  (
  `Username` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Claims` int(11) NULL DEFAULT 0,
  PRIMARY KEY (`Username`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for votes_ip
-- ----------------------------
DROP TABLE IF EXISTS `votes_ip`;
CREATE TABLE `votes_ip`  (
  `Date` datetime(0) NOT NULL,
  `IP` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
