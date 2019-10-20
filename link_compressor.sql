/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50728
Source Host           : localhost:3306
Source Database       : link_compressor

Target Server Type    : MYSQL
Target Server Version : 50728
File Encoding         : 65001

Date: 2019-10-20 18:03:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for url_catalog
-- ----------------------------
DROP TABLE IF EXISTS `url_catalog`;
CREATE TABLE `url_catalog` (
`ID`  bigint(20) NOT NULL AUTO_INCREMENT ,
`LONG_URL`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL ,
`CREATION_DATE`  datetime NULL DEFAULT NULL ,
`REDIRECT_COUNT`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`ID`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=12

;

-- ----------------------------
-- Auto increment value for url_catalog
-- ----------------------------
ALTER TABLE `url_catalog` AUTO_INCREMENT=12;
