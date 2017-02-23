<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Default" %>

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
						<h1 id="fh5co-logo"><a href="index.html">XUNLING迅灵信息</a></h1>
						<!-- START #fh5co-menu-wrap -->
						<nav id="fh5co-menu-wrap" role="navigation">
							<ul class="sf-menu" id="fh5co-primary-menu">
								<li><a href="work.html">登录</a></li>
								<li><a href="blog.html">注册</a></li>
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
			       					<p><a href="#" class="btn btn-primary btn-lg">迅灵来帮忙</a></p>
			       				</div>
			       			</div>
			       		</div>
			       	</div>
		  	    </div>
             </div>
		</aside>
		<div id="fh5co-portfolio-section">
			<div class="portfolio-row-half">
				<div class="portfolio-grid-item-color">
					<div class="desc">
						<h2>我们的服务</h2>
						<p>专业技术人员提供一对一远程协助服务</p>
						<p><a href="#" class="btn btn-primary btn-outline with-arrow">查看所有服务 <i class="icon-arrow-right22"></i></a></p>
					</div>
				</div>
				<a href="#" class="portfolio-grid-item" style="background-position: center center; background-image: url('images/digital1.png'); background-repeat: no-repeat; ">
					<div class="desc2">
						<h3>手机有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digital2.png);">
					<div class="desc2">
						<h3>电脑有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digital3.png);">
					<div class="desc2">
						<h3>互联网电视有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digital4.jpg);">
					<div class="desc2">
						<h3>网络游戏有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digital5.png);">
					<div class="desc2">
						<h3>数码相机有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digital6.png);">
					<div class="desc2">
						<h3>Shoes</h3>
						<span>平板有问题？</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#" class="portfolio-grid-item" style="background-image: url(images/digitalLogo.png);">
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
							<hr>
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
							<hr>
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
							<hr>
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
							<hr>
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
		<div class="fh5co-parallax fh5co-parallax2" style="background-image: url(images/hero.jpg);" data-stellar-background-ratio="0.5">
			<div class="overlay"></div>
			<div class="container">
				<div class="row">
					<div class="col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 col-xs-12 col-xs-offset-0 text-center fh5co-table">
						<div class="fh5co-intro animate-box">
							<div class="row">
								<div class="col-md-6 text-center">
									<div class="testimony">
										<span class="quote"><i class="icon-quotes-right"></i></span>
										<blockquote>
											<p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean.</p>
											<span>Athan Smith, via <a href="#" class="icon-twitter3 twitter-color"></a></span>
										</blockquote>
									</div>
								</div>
								<div class="col-md-6 text-center">
									<div class="testimony">
										<span class="quote"><i class="icon-quotes-right"></i></span>
										<blockquote>
											<p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean.</p>
											<span>Athan Smith, via <a href="#" class="icon-google-plus googleplus-color"></a></span>
										</blockquote>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div id="fh5co-blog-section" class="grey-bg">
			<div class="container">
				<div class="row">
					<div class="col-md-6 col-md-offset-3 text-center fh5co-heading">
						<i class="sl-icon-note"></i>
						<h2>Recent Blog</h2>
						<p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. </p>
					</div>
				</div>
				<div class="row">
					<div class="feature-full-1col">
						<div class="image" style="background-image: url(images/project-1.jpg);">
						</div>
						<div class="desc">
							<h3>New iPhone 7</h3>
							<p>Pellentesque habitant morbi tristique senectus et netus ett mauada fames ac turpis egestas. Etiam euismod tempor leo, in suscipit urna condimentum sed. Vivamus augue enim, consectetur ac interdum a, pulvinar ac massa. Nullam malesuada congue </p>
							<p><a href="#" class="btn btn-primary btn-luxe-primary">Learn More</a></p>
						</div>
					</div>

				<div class="feature-full-2col">
					<div class="blog-inner">
						<div class="image" style="background-image: url(images/project-2.jpg);">
						</div>
						<div class="desc">
							<h3>New iPhone 7</h3>
							<p>Pellentesque habitant morbi tristique senectus et netus ett mauada fames ac turpis egestas. Etiam euismod tempor leo, 
							in suscipit urna condimentum sed. </p>
							<p><a href="#" class="btn btn-primary btn-luxe-primary">Learn More</a></p>
						</div>
					</div>
					<div class="blog-inner">
						<div class="image" style="background-image: url(images/project-3.jpg);">
						</div>
						<div class="desc">
							<h3>Games Apps 2016</h3>
							<p>Pellentesque habitant morbi tristique senectus et netus ett mauada fames ac turpis egestas. Etiam euismod tempor leo, in suscipit urna condimentum sed. </p>
							<p><a href="#" class="btn btn-primary btn-luxe-primary">Learn More</a></p>
						</div>
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
							<h1 class="text-center">We Customize Your Template</h1>
							<p>Made with love by the fine folks at <a href="#">Freehtml5</a></p>
							<p><a href="#" class="btn btn-primary btn-lg">Get started</a></p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div id="fh5co-counter-section" class="fh5co-counters">
			<div class="container">
				<div class="row">
					<div class="col-md-6 col-md-offset-3 text-center fh5co-heading">
						<i class="sl-icon-badge"></i>
						<h2>Achievements</h2>
						<p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. </p>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3 text-center">
						<span class="fh5co-counter js-counter" data-from="0" data-to="1356" data-speed="5000" data-refresh-interval="50"></span>
						<span class="fh5co-counter-label">Cups of Coffee</span>
					</div>
					<div class="col-md-3 text-center">
						<span class="fh5co-counter js-counter" data-from="0" data-to="7290" data-speed="5000" data-refresh-interval="50"></span>
						<span class="fh5co-counter-label">Client</span>
					</div>
					<div class="col-md-3 text-center">
						<span class="fh5co-counter js-counter" data-from="0" data-to="4370" data-speed="5000" data-refresh-interval="50"></span>
						<span class="fh5co-counter-label">Projects</span>
					</div>
					<div class="col-md-3 text-center">
						<span class="fh5co-counter js-counter" data-from="0" data-to="4000" data-speed="5000" data-refresh-interval="50"></span>
						<span class="fh5co-counter-label">Finished Projects</span>
					</div>
				</div>
			</div>
		</div>
		<footer id="footer">
			<div class="container">
				<div class="row">
					<div class="col-md-4">
						<div class="copyright">
							<p><small>&copy; 2016 Free HTML5 <a href="index.html">Energy</a>. All Rights Reserved. <br> Made with <i class="icon-heart3"></i> by <a href="#/" target="_blank">Freehtml5</a> / More Templates <a href="http://www.cssmoban.com/" target="_blank" title="模板之家">模板之家</a> - Collect from <a href="http://www.cssmoban.com/" title="网页模板" target="_blank">网页模板</a></small></p>
						</div>
					</div>
					<div class="col-md-6">
						<div class="row">
							<div class="col-md-4">
								<h3>Company</h3>
								<ul class="link">
									<li><a href="#">About Us</a></li>
									<li><a href="#">Energy</a></li>
									<li><a href="#">Customer Care</a></li>
									<li><a href="#">Contact Us</a></li>
								</ul>
							</div>
							<div class="col-md-6">
								<h3>Subscribe</h3>
								<p>Far far away, behind the word mountains, far from the countries</p>
								<form action="#" id="form-subscribe">
									<div class="form-field">
										<input type="email" placeholder="Email Address" id="email">
										<input type="submit" id="submit" value="Send">
									</div>
								</form>
							</div>
						</div>
					</div>
					<div class="col-md-2">
						<ul class="social-icons">
							<li>
								<a href="#"><i class="icon-twitter-with-circle"></i></a>
								<a href="#"><i class="icon-facebook-with-circle"></i></a>
								<a href="#"><i class="icon-instagram-with-circle"></i></a>
								<a href="#"><i class="icon-linkedin-with-circle"></i></a>
							</li>
						</ul>
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
