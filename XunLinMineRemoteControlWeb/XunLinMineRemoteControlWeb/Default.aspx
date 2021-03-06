﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<!-- Animate.css -->
	<link rel="stylesheet" href="css/animate.css" type="text/css" />
	<!-- Icomoon Icon Fonts-->
	<link rel="stylesheet" href="css/icomoon.css" type="text/css" />
	<!-- Simple Line Icons -->
	<link rel="stylesheet" href="css/simple-line-icons.css" type="text/css" />
	<!-- Bootstrap  -->
	<link rel="stylesheet" href="css/bootstrap.css" type="text/css" />
	<!-- Superfish -->
	<link rel="stylesheet" href="css/superfish.css" type="text/css" />
	<!-- Flexslider  -->
	<link rel="stylesheet" href="css/flexslider.css" type="text/css" />

    <link rel="stylesheet" href="css/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
		<div id="fh5co-wrapper">
	    	<div id="fh5co-page">
		<div id="fh5co-header">
			<header id="fh5co-header-section">
				<div class="container">
					<div class="nav-header">
						<a href="#" class="js-fh5co-nav-toggle fh5co-nav-toggle"><i></i></a>
						<h1 id="fh5co-logo"><a href="#">XUNLING迅灵信息</a></h1>
						<!-- START #fh5co-menu-wrap -->
						<nav id="fh5co-menu-wrap" role="navigation">
							<ul class="sf-menu" id="fh5co-primary-menu">
								<li><a href="#">登录</a></li>
								<li><a href="#">注册</a></li>
							</ul>
						</nav>
					</div>
				</div>
			</header>		
		</div>
		<!-- end:fh5co-header -->
		<aside id="fh5co-hero">
			<div class="flexslider">
                <div class="slides">
			       	<div class="flex-active-slide" style="background-image: url('images/pain1.JPG');">
			       		<div class="overlay-gradient"></div>
			       		<div class="container">
			       			<div class="col-md-10 col-md-offset-1 text-center slider-text">
			       				<div class="slider-text-inner">
			       					<h2><b>自己搞不定?</b></h2>
			       					<p><a href="#server-list" class="btn btn-primary btn-lg">迅灵来帮忙</a></p>
			       				</div>
			       			</div>
			       		</div>
			       	</div>
		  	    </div>
             </div>
		</aside>
		<div id="fh5co-portfolio-section">
			<div class="portfolio-row-half">
				<div id="server-list" class="portfolio-grid-item-color">
					<div class="desc">
						<h2>我们的服务</h2>
						<p>专业技术人员提供一对一远程协助服务</p>
						<p><a href="#server1" class="btn btn-primary btn-outline with-arrow">查看所有服务 <i class="icon-arrow-right22"></i></a></p>
					</div>
				</div>
				<a id="server1" href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-position: center center; background-image: url('images/digital1.png'); background-repeat: no-repeat; ">
					<div class="desc2">
						<h3>手机有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital2.png);">
					<div class="desc2">
						<h3>电脑有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital3.png);">
					<div class="desc2">
						<h3>互联网电视有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital4.jpg);">
					<div class="desc2">
						<h3>网络游戏有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital5.png);">
					<div class="desc2">
						<h3>数码相机有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital6.png);">
					<div class="desc2">
						<h3>Shoes</h3>
						<span>平板有问题？</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digitalLogo.png);">
					<div class="desc2">
						<h3>其它任何数码问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
			</div>
		</div>
		<div id="fh5co-pricing-section">
			<div class="container">
				<div class="row">
					<div class="col-md-6 col-md-offset-3 text-center fh5co-heading">
						<i class="sl-icon-wallet"></i>
						<h2>价格</h2>
						<p>可以选择单次或长期服务，客户满意是我们的宗旨！</p>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<h2 class="pricing-plan">单次</h2>
							<div class="price"><sup class="currency">￥</sup>50<small></small></div>
							<p>提供单次服务</p>
							<hr/>
							<ul class="pricing-info">
								<li>远程清理电脑</li>
								<li>远程清理手机</li>
								<li>远程杀毒</li>
								<li>电脑加速</li>
							</ul>
							<p><asp:Button runat="server" ID="btnBuyOnce" class="btn btn-primary" Text="购买"></asp:Button></p>
						</div>
					</div>
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<h2 class="pricing-plan">一个月期</h2>
							<div class="price"><sup class="currency">￥</sup>300<small></small></div>
							<p>提供一个月内长期服务</p>
							<hr/>
							<ul class="pricing-info">
								<li>手机</li>
								<li>电脑</li>
								<li>网络</li>
								<li>其它数码产品</li>
							</ul>
							<p><asp:Button runat="server" ID="btnBuyOneMonth" class="btn btn-primary" Text="购买"></asp:Button></p>
						</div>
					</div>
					<div class="clearfix visible-sm-block"></div>
					<div class="col-md-3 col-sm-6 to-animate">
						<div class="price-box popular">
							<div class="popular-text">最佳选择</div>
							<h2 class="pricing-plan">半年期</h2>
							<div class="price"><sup class="currency">￥</sup>2000<small></small></div>
							<p>全天候专人为您提供服务</p>
							<hr/>
							<ul class="pricing-info">
								<li>手机</li>
								<li>电脑</li>
								<li>网络</li>
								<li>其它数码产品</li>
							</ul>
							<p><asp:Button runat="server" ID="btnBuyHalfYear" class="btn btn-primary" Text="购买"></asp:Button></p>
						</div>
					</div>
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<h2 class="pricing-plan">一年期</h2>
							<div class="price"><sup class="currency">￥</sup>5000<small></small></div>
							<p>全天候专人为您提供服务</p>
							<hr/>
							<ul class="pricing-info">
								<li>手机</li>
								<li>电脑</li>
								<li>网络</li>
								<li>其它数码产品</li>
							</ul>
							<p><asp:Button runat="server" ID="btnBuyOneYear" class="btn btn-primary" Text="购买"></asp:Button></p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="fh5co-parallax" style="background-image: url(images/hero2.jpg);" data-stellar-background-ratio="0.5">
			<div class="overlay"></div>
			<div class="container">
				<div class="row">
					<div class="col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 col-xs-12 col-xs-offset-0 text-center fh5co-table">
						<div class="fh5co-intro fh5co-table-cell animate-box">
							<h1 class="text-center"><b>解决您的所有难题</b></h1>
							<p>足不出户，让您搞定一切数码产品</p>
							<p><a href="#fh5co-pricing-section" class="btn btn-primary btn-lg">开始吧</a></p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<footer id="footer">
			<div class="container">
				<div class="row">
                    <div class="col-md-4">

                    </div>
					<div class="col-md-6">
						<div class="copyright">
							<p><small>&copy; 2017 盐山迅灵信息服务有限公司. 版权所有. </small></p>
						</div>
					</div>
				</div>
			</div>
		</footer>
	

	</div>
	        <!-- END fh5co-page -->

	    </div>
        <div>
        
        </div>
    </form>

	<!-- jQuery -->

	<script src="Scripts/jquery.min.js"></script>
	<!-- jQuery Easing -->
	<script src="Scripts/jquery.easing.1.3.js"></script>
	<!-- Bootstrap -->
	<script src="Scripts/bootstrap.min.js"></script>
	<!-- Waypoints -->
	<script src="Scripts/jquery.waypoints.min.js"></script>
	<!-- Superfish -->
	<script src="Scripts/hoverIntent.js"></script>
	<script src="Scripts/superfish.js"></script>
	<!-- Flexslider -->
	<script src="Scripts/jquery.flexslider-min.js"></script>
	<!-- Stellar -->
	<script src="Scripts/jquery.stellar.min.js"></script>
	<!-- Counters -->
	<script src="Scripts/jquery.countTo.js"></script>

	<!-- Main JS (Do not remove) -->
	<script src="Scripts/main.js"></script>

    </body>
</html>
