CREATE TABLE `alipayrecharge_exception_record` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `out_trade_no` varchar(35) NOT NULL,
  `alipay_trade_no` varchar(45) NOT NULL,
  `user_name` varchar(64) DEFAULT NULL,
  `buyer_email` varchar(35) NOT NULL,
  `total_fee` float NOT NULL,
  `value_rmb` float NOT NULL,
  `pay_time` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `out_trade_no_UNIQUE` (`out_trade_no`),
  UNIQUE KEY `alipay_trade_no_UNIQUE` (`alipay_trade_no`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
