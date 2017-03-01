<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital6.png);">
					<div class="desc2">
						<h3>互联网电视有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/dota.jpg);">
					<div class="desc2">
						<h3>网络游戏有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital4.jpg);">
					<div class="desc2">
						<h3>数码相机有问题？</h3>
						<span>找迅灵</span>
						<i class="sl-icon-heart"></i>
					</div>
				</a>
				<a href="#fh5co-pricing-section" class="portfolio-grid-item" style="background-image: url(images/digital7.jpg);">
					<div class="desc2">
						<h3>监控有问题？</h3>
						<span>找迅灵</span>
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
							<p>24小时内只能购买一次，可累计</p>
							<hr/>
							<div class="pricing-info">
								定向服务，一次付费，满意为止，
无论是电视、电脑、手机、数码相机、游戏机、ipad、watch、ipod、iTouch、投影、LED、监控，两个工作日内应答解决，软件问题在线解决，硬件问题线下解决，为您的设备保驾护航。
							</div>
							<p><asp:Button runat="server" ID="btnBuyOnce" class="btn btn-primary" Text="购买" OnClick="btnBuyOnce_Click"></asp:Button></p>
						</div>
					</div>
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<h2 class="pricing-plan">一个月期</h2>
							<div class="price"><sup class="currency">￥</sup>300<small></small></div>
							<p>为个人用户，提供一个月长期服务</p>
							<hr/>
							<div class="pricing-info">
								包月服务时长为30天，从付费起计时，不限制地域，不限制设备 ，不限制网络，只要是您的呼唤，我们都可以为您服务，24小时 应答确保您的设备完善运转。
							</div>
							<p><asp:Button runat="server" ID="btnBuyOneMonth" class="btn btn-primary" Text="购买" OnClick="btnBuyOneMonth_Click"></asp:Button></p>
						</div>
					</div>
					<div class="clearfix visible-sm-block"></div>
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<%--<div class="popular-text">最佳选择</div>--%>
							<h2 class="pricing-plan">一季度</h2>
							<div class="price"><sup class="currency">￥</sup>2000<small></small></div>
							<p>为企业用户，提供一季度长期服务</p>
							<hr/>
							<div class="pricing-info">
								服务时长为90天，服务需登记您的企业信息，包括您的员工数量 ，公用、私用的数码设备数量，只提供在线类服务，只要是您的 线上需求都可以尽量满足，付费后有您的专员为您答疑一切数码问题。 
							</div>
							<p><asp:Button runat="server" ID="btnBuyThreeMonth" class="btn btn-primary" Text="购买" OnClick="btnBuyThreeMonth_Click"></asp:Button></p>
						</div>
					</div>
					<div class="col-md-3 col-sm-6">
						<div class="price-box to-animate">
							<h2 class="pricing-plan">一年期</h2>
							<div class="price"><sup class="currency">￥</sup>5000<small></small></div>
							<p>365天全天候专人为您提供服务</p>
							<hr/>
							<div class="pricing-info">
								服务时长为365天，提供一切线上服务，包括网站、程序等周边服务，开设贵宾级服务特权，包括私人专员为您服务，专属热线开通，7*24应答您的需求，必要情况下提供上门服务，更多特权期待您的开通。
							</div>
							<p><asp:Button runat="server" ID="btnBuyOneYear" class="btn btn-primary" Text="购买" OnClick="btnBuyOneYear_Click"></asp:Button></p>
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
</asp:Content>
