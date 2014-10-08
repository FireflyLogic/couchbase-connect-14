using System;
using System.Linq;

namespace cbconnectcli
{
	public class ExtractSpeakerImageUrlCommand : ICommand
	{
		public ExtractSpeakerImageUrlCommand ()
		{

		}

		public void Run ()
		{
			var blocks = GetContent ()
				.Split (new [] { "<!-- .speaker-wrap -->" }, StringSplitOptions.RemoveEmptyEntries)
				.Where (x => x.StartsWith ("\n\t\t\t<div id=\"speaker-"))
				.ToList ();

			var cells = blocks.Select (x => x.Split (new [] { 
				"style=\"background-image: url('", 
				"')\"></div>\n", 
				"<h3>", "</h3>"}, StringSplitOptions.None)).ToList();

			foreach (var item in cells)
			{
				Console.WriteLine (item [3] + "|" + item [1]);
			}
			
		}

		private struct SpeakerImage
		{
			string Id { get; set;}
			string Url { get; set;}
		}

		private static string GetContent()
		{
			return @"
<!DOCTYPE html>
<html lang=""en-US"" prefix=""og: http://ogp.me/ns#"">
<head>
	<meta charset=""UTF-8"" />
	<meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
	<title>Speakers &ndash; Couchbase</title>
	<link rel=""profile"" href=""http://gmpg.org/xfn/11"" />
	<link rel=""pingback"" href=""http://www.couchbase.com/connect/xmlrpc.php"" />
	
<!-- This site is optimized with the Yoast WordPress SEO plugin v1.5.6 - https://yoast.com/wordpress/plugins/seo/ -->
<link rel=""canonical"" href=""http://www.couchbase.com/connect/speakers/"" />
<meta property=""og:locale"" content=""en_US"" />
<meta property=""og:type"" content=""article"" />
<meta property=""og:title"" content=""Speakers &ndash; Couchbase"" />
<meta property=""og:description"" content=""Who&#8217;s speaking"" />
<meta property=""og:url"" content=""http://www.couchbase.com/connect/speakers/"" />
<meta property=""og:site_name"" content=""Couchbase"" />
<!-- / Yoast WordPress SEO plugin. -->

<link rel=""alternate"" type=""application/rss+xml"" title=""Couchbase &raquo; Feed"" href=""http://www.couchbase.com/connect/feed/"" />
<link rel=""alternate"" type=""application/rss+xml"" title=""Couchbase &raquo; Comments Feed"" href=""http://www.couchbase.com/connect/comments/feed/"" />
<link rel=""alternate"" type=""text/calendar"" title=""Couchbase &raquo; iCal Feed"" href=""http://www.couchbase.com/connect/agenda/?ical=1"" />
<link rel=""alternate"" type=""application/rss+xml"" title=""Couchbase &raquo; Speakers Comments Feed"" href=""http://www.couchbase.com/connect/speakers/feed/"" />
<link rel='stylesheet' id='open-sans-css'  href='//fonts.googleapis.com/css?family=Open+Sans%3A300italic%2C400italic%2C600italic%2C300%2C400%2C600&#038;subset=latin%2Clatin-ext&#038;ver=4.0' type='text/css' media='all' />
<link rel='stylesheet' id='fjalla-css'  href='//fonts.googleapis.com/css?family=Kreon%3A300%2C400%2C700&#038;ver=4.0' type='text/css' media='all' />
<link rel='stylesheet' id='kreon-css'  href='//fonts.googleapis.com/css?family=Kreon%3A300%2C400%2C700&#038;ver=4.0' type='text/css' media='all' />
<link rel='stylesheet' id='bootstrapwp-css'  href='http://www.couchbase.com/connect/wp-content/themes/couchbase/bootstrapwp.css?ver=4.0' type='text/css' media='all' />
<link rel='stylesheet' id='fancybox-css'  href='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/fancybox/jquery.fancybox.css?ver=4.0' type='text/css' media='all' />
<link rel='stylesheet' id='style-css'  href='http://www.couchbase.com/connect/wp-content/themes/couchbase/style.css?ver=4.0' type='text/css' media='all' />
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-includes/js/jquery/jquery.js?ver=1.11.1'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-includes/js/jquery/jquery-migrate.min.js?ver=1.2.1'></script>
<link rel=""EditURI"" type=""application/rsd+xml"" title=""RSD"" href=""http://www.couchbase.com/connect/xmlrpc.php?rsd"" />
<link rel=""wlwmanifest"" type=""application/wlwmanifest+xml"" href=""http://www.couchbase.com/connect/wp-includes/wlwmanifest.xml"" /> 
<meta name=""generator"" content=""WordPress 4.0"" />
<link rel='shortlink' href='http://www.couchbase.com/connect/?p=7' />
	<style type=""text/css"">.recentcomments a{display:inline !important;padding:0 !important;margin:0 !important;}</style>
	<link type=""text/css"" rel=""stylesheet"" href=""http://fast.fonts.net/cssapi/7d00c06c-55da-4e88-b746-3780a9bc81ce.css""/>
	<style type=""text/css"">
								
            .tribe-events-category-architecture .tribe-events-event-category { background-color: #8224e3; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-architecture { border-left-color: #8224e3; }
		    
            						
            .tribe-events-category-customer .tribe-events-event-category { background-color: #1e73be; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-customer { border-left-color: #1e73be; }
		    
            						
            .tribe-events-category-developer .tribe-events-event-category { background-color: #81d742; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-developer { border-left-color: #81d742; }
		    
            						
            .tribe-events-category-enterprise .tribe-events-event-category { background-color: #eeee22; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-enterprise { border-left-color: #eeee22; }
		    
            						
            .tribe-events-category-general .tribe-events-event-category { background-color: #cccccc; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-general { border-left-color: #cccccc; }
		    
            						
            .tribe-events-category-mobile .tribe-events-event-category { background-color: #dd9933; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-mobile { border-left-color: #dd9933; }
		    
            						
            .tribe-events-category-operations .tribe-events-event-category { background-color: #dd3333; }
            #overview.cols-wrap .cell .day-row .title.tribe-events-category-operations { border-left-color: #dd3333; }
		    
            	</style>
</head>
<body class=""page page-id-7 page-template page-template-tpl-speakers-php tribe-theme-couchbase"">

<div id=""main-wrap"">
		<div id=""header"">
				<div id=""wordmark"">
					<a href=""http://www.couchbase.com/connect/"" rel=""home"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/CBC_updatedLogoDatesHeaderIMG.011.png');"">
						Couchbase					</a>
				</div><!-- #wordmark -->
									<h1 id=""header-blurb"">OCTOBER 6TH - 7TH, 2014<br />
SAN FRANCISCO, CA</h1>
								<div id=""nav-wrapper-new"">
						<div class=""nav-collapse"">
								<ul id=""menu-main"" class=""nav""><li id=""menu-item-12"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-12""><a href=""http://www.couchbase.com/connect/"">Home</a></li>
<li id=""menu-item-74"" class=""menu-item menu-item-type-custom menu-item-object-custom menu-item-74""><a href=""http://www.couchbase.com/connect/agenda/2014-10-06/"">Agenda</a></li>
<li id=""menu-item-16"" class=""menu-item menu-item-type-post_type menu-item-object-page current-menu-item page_item page-item-7 current_page_item menu-item-16""><a href=""http://www.couchbase.com/connect/speakers/"">Speakers</a></li>
<li id=""menu-item-17"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-17""><a href=""http://www.couchbase.com/connect/sponsors/"">Sponsors</a></li>
<li id=""menu-item-19"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-19""><a href=""http://www.couchbase.com/connect/attend/"">Why Attend</a></li>
<li id=""menu-item-13"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-13""><a href=""http://www.couchbase.com/connect/accommodations/"">Accommodations</a></li>
<li id=""menu-item-18"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-18""><a href=""http://www.couchbase.com/connect/training/"">Training</a></li>
<li id=""menu-item-31"" class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-31""><a href=""http://www.couchbase.com/connect/register/"">Register</a></li>
</ul>						</div>
				</div>
		</div><!-- #header -->
		<div class=""navbar navbar-inverse navbar-relative-top"">
				<div class=""navbar-inner"">
						<div class=""container"">
								<button type=""button"" class=""btn btn-navbar"" data-toggle=""collapse"" data-target="".nav-collapse""> <span class=""icon-bar""></span> <span class=""icon-bar""></span> <span class=""icon-bar""></span> </button>
								<a class=""brand"" href=""http://www.couchbase.com/connect/"" title=""Couchbase"" rel=""home"">
																			<img src=""http://www.couchbase.com/connect/wp-content/uploads/2014/09/CBC_updatedLogoDatesHeaderIMG.011.png"" alt=""Couchbase"" />
																	</a>
								<div class=""nav-collapse"">
										<ul id=""menu-main-1"" class=""nav""><li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-12""><a href=""http://www.couchbase.com/connect/"">Home</a></li>
<li class=""menu-item menu-item-type-custom menu-item-object-custom menu-item-74""><a href=""http://www.couchbase.com/connect/agenda/2014-10-06/"">Agenda</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page current-menu-item page_item page-item-7 current_page_item menu-item-16""><a href=""http://www.couchbase.com/connect/speakers/"">Speakers</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-17""><a href=""http://www.couchbase.com/connect/sponsors/"">Sponsors</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-19""><a href=""http://www.couchbase.com/connect/attend/"">Why Attend</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-13""><a href=""http://www.couchbase.com/connect/accommodations/"">Accommodations</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-18""><a href=""http://www.couchbase.com/connect/training/"">Training</a></li>
<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-31""><a href=""http://www.couchbase.com/connect/register/"">Register</a></li>
</ul>								</div>
						</div>
				</div>
		</div><!-- .navbar -->
		<div id=""carousel"" class=""section"">
										<div id=""hud-container"" class=""fixed-width centered"">
								<div id=""weather-hud"" class=""hud"">
										<div id=""weather-left-col"">
												<h2 style=""text-align: center""><strong>Couchbase Connect Conference</strong><br />
Next Generation NoSQL for the Enterprise</h2>
										</div>
								</div>
						</div><!-- #hud-container -->
														<div id=""carousel-footer"">
								<div class=""big-button"">
																					<h4>OCTOBER 6TH - 7TH, 2014 | San Francisco</h4>
																															<a href=""http://www.couchbase.com/connect/register"">
												Free Pass - Register Now!											</a>
																		</div>
						</div><!-- #carousel-footer -->
								<div id=""carousel-fade""></div>
								<div id=""carousel-background-img"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/CBC_newSite.header02.02.jpg');""></div>
		</div><!-- #carousel -->


<div class=""section centered"">
					<article id=""post-7"" class=""post-7 page type-page status-publish hentry"">
				<div class=""entry-content"">
					<h2>Who&#8217;s speaking</h2>
						<div id=""speakers-icons-wrap"" class=""centered"">
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-412"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-412"">Chris Bertsch</a></h3>
																						<div class=""company"">FactSet</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-412"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Chris Bertsch</h3>
																			<div class=""company"">FactSet</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
																				</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-188"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sam-basu.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-188"">Samidip Basu</a></h3>
																						<div class=""company"">Telerik</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-188"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sam-basu.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Samidip Basu</h3>
																			<div class=""company"">Telerik</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Developer Advocate</strong></p>
<p>Samidip Basu (@samidip) is a technologist, Apress/Pluralsight author, speaker, Microsoft MVP, believer in software craftsmanship, gadget-lover and Developer Advocate for Telerik. With a long developer background, he now spends much of his time advocating modern web/mobile/cloud development platforms on Microsoft/Telerik stacks. He passionately helps run The Windows Developer User Group (http://thewindowsdeveloperusergroup.com/), labors in M3 Conf (http://m3conf.com/) organization and can be found with at-least a couple of hobbyist projects at any time. His spare times call for travel and culinary adventures with the wife. Find out more at http://samidipbasu.com.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/samidip"">@samidip</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-232"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Screen-Shot-2014-09-04-at-6.51.46-PM.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-232"">Tjeerd Brenninkmeijer</a></h3>
																						<div class=""company"">Hippo CMS</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-232"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Screen-Shot-2014-09-04-at-6.51.46-PM.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Tjeerd Brenninkmeijer</h3>
																			<div class=""company"">Hippo CMS</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CMO</strong></p>
<p>Tjeerd Brenninkmeijer is CMO and co-founder of Hippo. He looks after the company&#8217;s strategic alliances and has extensive experience as an international speaker. Before Hippo, Tjeerd worked as a consultant for KPMG. He has a degree in Economics from the University of Amsterdam.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/tbrenninkmeijer"">@tbrenninkmeijer</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-236"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-236"">Ben Christensen</a></h3>
																						<div class=""company"">Netflix</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-236"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Ben Christensen</h3>
																			<div class=""company"">Netflix</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Edge Engineering</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-37"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/scott-feinberg-wepay-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-37"">Scott Feinberg</a></h3>
																						<div class=""company"">WePay</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-37"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/scott-feinberg-wepay-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Scott Feinberg</h3>
																			<div class=""company"">WePay</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong style=""color: #000000;"">Developer Evangelist</strong><span style=""color: #000000;""><br />
</span></p>
<p>Scott Feinberg is the Developer Evangelist at WePay, where he works with developers who want to allow their customers to collect payments to make sure their experience with the WePay API as seamless and easy as possible. Scott&#8217;s designed and deployed the infrastructure for many different startup companies using a wide variety of tools.  When Scott is not meeting with developers and customers across the US, he&#8217;s likely traveling to some new exotic place.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-404"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Rafael1-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-404"">Rafael Godinho</a></h3>
																						<div class=""company"">Microsoft</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-404"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Rafael1-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Rafael Godinho</h3>
																			<div class=""company"">Microsoft</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Technical Evangelist</strong></p>
<p>Rafael Godinho is a Technical Evangelist at Microsoft. In this role, he supports customers and partners to best adopt Microsoft Azure cloud platform. He holds a Business Management MBA and Computer Engineering Bachelor degree. Rafael has been working with software development more time than he would like to remember and less time than he would like to have, and has been talking so much about cloud sometimes it is hard to keep his feet on the ground.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-262"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-262"">Josh Greenbaum</a></h3>
																						<div class=""company"">Enterprise Application Consulting</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-262"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Josh Greenbaum</h3>
																			<div class=""company"">Enterprise Application Consulting</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
																						<a class=""speaker-twitter"" href=""https://twitter.com/josheac"">@josheac</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-364"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Will_Hayes.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-364"">Will Hayes</a></h3>
																						<div class=""company"">LucidWorks</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-364"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Will_Hayes.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Will Hayes</h3>
																			<div class=""company"">LucidWorks</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CEO</strong></p>
<p>Will Hayes is CEO of Lucidworks, a San Francisco-based enterprise search company. Lucidworks provides the search platform for the world&#8217;s biggest brands, built on the open core, open source search project Apache Solr. Previously, Will has worked at big data analysis company Splunk and biotech firm Genentech.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-417"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-417"">Ben Henderson</a></h3>
																						<div class=""company"">Firefly Logic</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-417"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Ben Henderson</h3>
																			<div class=""company"">Firefly Logic</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
																				</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-244"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Alex-Heneveld1.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-244"">Alex Heneveld</a></h3>
																						<div class=""company"">Cloudsoft</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-244"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Alex-Heneveld1.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Alex Heneveld</h3>
																			<div class=""company"">Cloudsoft</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CTO</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-247"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/bryan-1-large.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-247"">Bryan Hunter</a></h3>
																						<div class=""company"">Firefly Logic</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-247"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/bryan-1-large.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Bryan Hunter</h3>
																			<div class=""company"">Firefly Logic</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CTO</strong></p>
<p>Bryan Hunter is the CTO of Firefly Logic and a cheerleader at NashFP (Nashville Functional Programmers). He is a Microsoft MVP in F# and a well-known figure in the Erlang community. Bryan has a long obsession with both Lean and functional programming.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/bryan_hunter"">@bryan_hunter</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-326"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-326"">Rahul Jain</a></h3>
																						<div class=""company"">AT&T</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-326"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Rahul Jain</h3>
																			<div class=""company"">AT&T</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Director of Engineering</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-33"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/chris-kasten-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-33"">Chris Kasten</a></h3>
																						<div class=""company"">eBay</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-33"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/chris-kasten-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Chris Kasten</h3>
																			<div class=""company"">eBay</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong style=""color: #000000;"">Chief Architect Global Data Infrastructure</strong><span style=""color: #000000;""><br />
</span></p>
<p>Chris Kasten is an architect and engineer with two decades of experience in building highly scalable software infrastructure and systems. His current gig is Distinguished Architect for the Global Data Infrastructure organization at eBay, Inc. Chris has significant domain expertise and knowledge in architecting and building highly scalable data access, caching, and database technology solutions. Mr. Kasten was one of the founders and the lead architect for the eBay custom Java data access layer and caching infrastructure, which processes over 200 billion database operations per day over 300+ sharded database. He has several patents both granted and pending relating to data access, caching, and database technologies. Mr. Kasten has a broad experience having worked for NASA, several fortune 500 companies in aerospace, telecom, and online commerce, as well as consulted independently.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-228"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/michaelkehoe-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-228"">Michael Kehoe</a></h3>
																						<div class=""company"">LinkedIn</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-228"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/michaelkehoe-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Michael Kehoe</h3>
																			<div class=""company"">LinkedIn</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Site Reliability Engineer</strong></p>
<p>Michael Kehoe is a Site Reliability Engineer at LinkedIn. As an SRE for Identity &amp; Higher-Education, Michael strives to create the best operational experience possible for SRE’s maintaining Couchbase clusters. Michael is currently leading LinkedIn’s exploration of using XDCR on production systems. His background is in Network Engineering with a major of Electrical Engineering at the University of Queensland, Australia.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-237"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Renat_Khasanshyn_1060x1060-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-237"">Renat Khasanshyn</a></h3>
																						<div class=""company"">Altoros</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-237"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Renat_Khasanshyn_1060x1060-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Renat Khasanshyn</h3>
																			<div class=""company"">Altoros</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CEO</strong></p>
<p>Renat Khasanshyn is CEO of Altoros and Venture Partner at Runa Capital. Renat is a subject matter expert on database infrastructure software and Cloud Foundry PaaS . Altoros brings Cloud Foundry-based ”software factories” and NoSQL-driven “data lakes” into organizations through training, deployment, and integration. With 250+ employees across 8 countries, Altoros is behind some of the world’s largest Cloud Foundry &amp; NoSQL deployments. Mr. Khasanshyn is also founder of Apatar, an open source data integration toolset, founder of Silicon Valley NewSQL User Group and co-founder of the Belarusian Java User Group. He studied Engineering at Belarusian National Technical University.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-367"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-367"">Kyle Lichtenberg</a></h3>
																						<div class=""company"">Amazon Web Services</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-367"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Kyle Lichtenberg</h3>
																			<div class=""company"">Amazon Web Services</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Solutions Architect</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-410"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-410"">Joe Lichtenberg</a></h3>
																						<div class=""company"">Mirror Image</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-410"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Joe Lichtenberg</h3>
																			<div class=""company"">Mirror Image</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>VP Advertising and Analytics</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-339"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-339"">Josh Long</a></h3>
																						<div class=""company"">Pivotal</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-339"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Josh Long</h3>
																			<div class=""company"">Pivotal</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Spring Developer Advocate</strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-329"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/rMayuram.Headshot.WEB_-e1410455471350-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-329"">Ravi Mayuram</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-329"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/rMayuram.Headshot.WEB_-e1410455471350-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Ravi Mayuram</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>VP of Engineering</strong></p>
<p>Ravi Mayuram is the Vice President of Engineering at Couchbase. Previously in his career, Ravi held senior technical and management positions at leading software companies including <span class=""caps"">BEA</span>, HP, Informix, Oracle, and Siebel Systems. He led innovations in the areas of social graph and search and analytics at Oracle, as well as helped initiate the company’s Cloud Collaboration Platform. Ravi’s startup experience includes BroadBand Office (Kleiner-Perkins venture) and Plumtree (acquired by <span class=""caps"">BEA).</span></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-35"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/anil-madan-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-35"">Anil Madan</a></h3>
																						<div class=""company"">PayPal</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-35"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/anil-madan-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Anil Madan</h3>
																			<div class=""company"">PayPal</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong style=""color: #000000;"">Senior Director, Engineering</strong><br />
Anil is the Sr. Director of Engineering at PayPal running several of their Online &amp; Offline systems around Personalization, PayPal Behavioral Analytics and Marketing. Prior to this he built out eBay’s Big Data Hadoop platform with an underlying infrastructure handling petabytes of data to power search relevance and other efforts around enhancing Trust, Value &amp; Selection. He has spent last 10 years in building distributed online big data systems. Anil has a Masters in Computers Science from Pune University and Bachelors in Physics from St Stephens College Delhi, India.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-225"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/carletonmiyamoto-e1409862304108-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-225"">Carleton Miyamoto</a></h3>
																						<div class=""company"">LinkedIn</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-225"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/carletonmiyamoto-e1409862304108-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Carleton Miyamoto</h3>
																			<div class=""company"">LinkedIn</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Engineering Tech Lead</strong></p>
<p>Carleton Miyamoto is a backend Software Engineering Tech Lead at LinkedIn. As a veteran coder, his recent work includes implementing and scaling high QPS systems, static analysis tools for XML, and custom ORM and API building libraries. His previous work includes writing compilers and compiler tools, server management software, and mobile Web development.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-235"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Holger.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-235"">Holger Mueller</a></h3>
																						<div class=""company"">Constellation Research</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-235"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Holger.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Holger Mueller</h3>
																			<div class=""company"">Constellation Research</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Analyst</strong></p>
<p><span data-sheets-value=""[null,2,&quot;Holger Mueller is a Vice President and Principal Analyst for the fundamental enablers of the cloud, IaaS and PaaS, with forays up the tech stack into BigData and Analytics and sometimes even SaaS. Mueller provides strategy and counsel to key Constellation Research clients, including Chief Information Officers, Chief Technology Officers, Chief Product Officers, investment analysts, venture capitalists, sell side firms and technology buyers. Prior to joining Constellation Research, Mueller was VP of Products for NorthgateArinso, a KKR company. There, he led the transformation of products to the cloud and laid the foundation for new Business Process as a Service (BPaaS) capabilities.Previously, Mueller was Chief Application Architect with SAP, working on strategic projects and next-generation product capabilities. Mueller was also VP of Products for FICO, creating the foundation for the current Enterprise Decision Management Suite. Before that, he worked for Oracle in various management functions both on the application development (CRM, Fusion) side and business development side. Previously, he worked for SAP, starting the products suite that is currently SAP CRM and worked in the Office of the Chairman for Hasso Plattner. Mueller started his career with Kiefer &amp; Veittinger, which he helped develop from a startup to Europe\u2019s largest CRM vendor from 1995 onwards. There, he helped spearhead offshore development in Bangalore, India, where the previous K&amp;V Lab is now SAP Labs Bangalore. Mueller has presented at numerous trade shows and industry events and writes his blog on enterprise software at enswmu.blogspot.com. Mueller has a Diplom Kaufmann from University of Mannheim, with a focus on Information Science, Marketing, International Management and Chemical Technology. As a native European, Mueller speaks six languages.\n&quot;]"" data-sheets-userformat=""[null,null,13059,[null,0],[null,2,16777215],null,null,null,null,null,null,1,0,null,null,&quot;arial,sans,sans-serif&quot;,10]"">Holger Mueller is a Vice President and Principal Analyst for the fundamental enablers of the cloud, IaaS and PaaS, with forays up the tech stack into BigData and Analytics and sometimes even SaaS. Mueller provides strategy and counsel to key Constellation Research clients, including Chief Information Officers, Chief Technology Officers, Chief Product Officers, investment analysts, venture capitalists, sell side firms and technology buyers. </span></p>
<p><span data-sheets-value=""[null,2,&quot;Holger Mueller is a Vice President and Principal Analyst for the fundamental enablers of the cloud, IaaS and PaaS, with forays up the tech stack into BigData and Analytics and sometimes even SaaS. Mueller provides strategy and counsel to key Constellation Research clients, including Chief Information Officers, Chief Technology Officers, Chief Product Officers, investment analysts, venture capitalists, sell side firms and technology buyers. Prior to joining Constellation Research, Mueller was VP of Products for NorthgateArinso, a KKR company. There, he led the transformation of products to the cloud and laid the foundation for new Business Process as a Service (BPaaS) capabilities.Previously, Mueller was Chief Application Architect with SAP, working on strategic projects and next-generation product capabilities. Mueller was also VP of Products for FICO, creating the foundation for the current Enterprise Decision Management Suite. Before that, he worked for Oracle in various management functions both on the application development (CRM, Fusion) side and business development side. Previously, he worked for SAP, starting the products suite that is currently SAP CRM and worked in the Office of the Chairman for Hasso Plattner. Mueller started his career with Kiefer &amp; Veittinger, which he helped develop from a startup to Europe\u2019s largest CRM vendor from 1995 onwards. There, he helped spearhead offshore development in Bangalore, India, where the previous K&amp;V Lab is now SAP Labs Bangalore. Mueller has presented at numerous trade shows and industry events and writes his blog on enterprise software at enswmu.blogspot.com. Mueller has a Diplom Kaufmann from University of Mannheim, with a focus on Information Science, Marketing, International Management and Chemical Technology. As a native European, Mueller speaks six languages.\n&quot;]"" data-sheets-userformat=""[null,null,13059,[null,0],[null,2,16777215],null,null,null,null,null,null,1,0,null,null,&quot;arial,sans,sans-serif&quot;,10]"">Prior to joining Constellation Research, Mueller was VP of Products for NorthgateArinso, a KKR company. There, he led the transformation of products to the cloud and laid the foundation for new Business Process as a Service (BPaaS) capabilities.Previously, Mueller was Chief Application Architect with SAP, working on strategic projects and next-generation product capabilities. Mueller was also VP of Products for FICO, creating the foundation for the current Enterprise Decision Management Suite. Before that, he worked for Oracle in various management functions both on the application development (CRM, Fusion) side and business development side. Previously, he worked for SAP, starting the products suite that is currently SAP CRM and worked in the Office of the Chairman for Hasso Plattner. Mueller started his career with Kiefer &amp; Veittinger, which he helped develop from a startup to Europe’s largest CRM vendor from 1995 onwards. There, he helped spearhead offshore development in Bangalore, India, where the previous K&amp;V Lab is now SAP Labs Bangalore. Mueller has presented at numerous trade shows and industry events and writes his blog on enterprise software at enswmu.blogspot.com. </span></p>
<p><span data-sheets-value=""[null,2,&quot;Holger Mueller is a Vice President and Principal Analyst for the fundamental enablers of the cloud, IaaS and PaaS, with forays up the tech stack into BigData and Analytics and sometimes even SaaS. Mueller provides strategy and counsel to key Constellation Research clients, including Chief Information Officers, Chief Technology Officers, Chief Product Officers, investment analysts, venture capitalists, sell side firms and technology buyers. Prior to joining Constellation Research, Mueller was VP of Products for NorthgateArinso, a KKR company. There, he led the transformation of products to the cloud and laid the foundation for new Business Process as a Service (BPaaS) capabilities.Previously, Mueller was Chief Application Architect with SAP, working on strategic projects and next-generation product capabilities. Mueller was also VP of Products for FICO, creating the foundation for the current Enterprise Decision Management Suite. Before that, he worked for Oracle in various management functions both on the application development (CRM, Fusion) side and business development side. Previously, he worked for SAP, starting the products suite that is currently SAP CRM and worked in the Office of the Chairman for Hasso Plattner. Mueller started his career with Kiefer &amp; Veittinger, which he helped develop from a startup to Europe\u2019s largest CRM vendor from 1995 onwards. There, he helped spearhead offshore development in Bangalore, India, where the previous K&amp;V Lab is now SAP Labs Bangalore. Mueller has presented at numerous trade shows and industry events and writes his blog on enterprise software at enswmu.blogspot.com. Mueller has a Diplom Kaufmann from University of Mannheim, with a focus on Information Science, Marketing, International Management and Chemical Technology. As a native European, Mueller speaks six languages.\n&quot;]"" data-sheets-userformat=""[null,null,13059,[null,0],[null,2,16777215],null,null,null,null,null,null,1,0,null,null,&quot;arial,sans,sans-serif&quot;,10]"">Mueller has a Diplom Kaufmann from University of Mannheim, with a focus on Information Science, Marketing, International Management and Chemical Technology. As a native European, Mueller speaks six languages.<br />
</span></p>
															<a class=""speaker-twitter"" href=""https://twitter.com/holgermu"">@holgermu</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-240"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/6o7kq8qx-400x400.jpeg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-240"">David Ostrovsky</a></h3>
																						<div class=""company"">Sela Group</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-240"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/6o7kq8qx-400x400.jpeg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>David Ostrovsky</h3>
																			<div class=""company"">Sela Group</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Senior Architect</strong></p>
<p>David Ostrovsky is a senior architect and trainer at Sela Group, with over 14 years of professional experience in the industry and extensive knowledge of software development, database administration, NoSQL and big data architecture. He is the co-author of the books &#8220;Pro Couchbase Server&#8221;, &#8220;Essentials of Developing Windows Store Apps&#8221; and &#8220;Advanced Windows Store App Development&#8221;, as well as numerous articles and courses.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/DavidOstrovsky"">@DavidOstrovsky</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-243"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-243"">Yannis Papakonstantinou</a></h3>
																						<div class=""company"">UCSD</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-243"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Yannis Papakonstantinou</h3>
																			<div class=""company"">UCSD</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
																				</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-242"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Kyle-Porter1-e1410806274258-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-242"">Kyle Porter</a></h3>
																						<div class=""company"">Simba Technologies</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-242"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Kyle-Porter1-e1410806274258-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Kyle Porter</h3>
																			<div class=""company"">Simba Technologies</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Solutions Engineer</strong></p>
<p>Kyle is a sales engineer with Simba Technologies in Vancouver, BC.  He is a seven-year veteran of Simba Technologies, first serving as a core developer on Simba&#8217;s relational data access team, and now leading sales engineering for customer and partner engagements. Kyle graduated with honors from University of British Columbia with a BSc in computer science.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-245"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Rajesh_Ramchandani1-400x400.jpeg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-245"">Rajesh Ramchandani</a></h3>
																						<div class=""company"">CumuLogic</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-245"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Rajesh_Ramchandani1-400x400.jpeg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Rajesh Ramchandani</h3>
																			<div class=""company"">CumuLogic</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Founder &amp; CTO</strong></p>
<p>CumuLogic founder and CTO, Rajesh Ramchandani has over 18 years of experience in the software industry, with over 10 years of hands-on experience in Java and middleware. He has extensive knowledge in HPC, Utility Computing and Cloud Computing technologies, and was part of the product management team for the Sun Cloud. Rajesh has an Electronics Engineering degree from Pune University, India.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-246"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Screen-Shot-2014-08-13-at-10.45.03-AM-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-246"">Monish Sharma</a></h3>
																						<div class=""company"">ElasticBox</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-246"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Screen-Shot-2014-08-13-at-10.45.03-AM-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Monish Sharma</h3>
																			<div class=""company"">ElasticBox</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CTO</strong></p>
<p>Monish is a senior techno functional leader, passionate about mapping technology solutions &#8211; people, process and product to business needs with focus on engineering excellence. He has 17 years experience in high tech, Fortune 100 and venture-backed technology startups. Monish has been successful in SaaS/PaaS service delivery experience architecting, scaling B2C (Shutterfly &amp; Microsoft Hotmail) and B2B enterprise software (Salesforce, Nexant &amp; Innotas).  In the past, he has hired, managed and mentored geo-dispersed high performance Engineering and Web Operations teams. Monish has incredibly successful startup and IPO experience (Shutterfly) and is experienced with SDLC leveraging Agile SCRUM methodology.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-351"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/highres_101051892-400x400.jpeg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-351"">Christopher Tse</a></h3>
																						<div class=""company"">McGraw Hill Labs</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-351"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/highres_101051892-400x400.jpeg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Christopher Tse</h3>
																			<div class=""company"">McGraw Hill Labs</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Sr. Director / Head of R&amp;D </strong></p>
															<a class=""speaker-twitter"" href=""https://twitter.com/christse"">@christse</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-248"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/derek_tumulak.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-248"">Derek Tumulak</a></h3>
																						<div class=""company"">Vormetric</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-248"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/derek_tumulak.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Derek Tumulak</h3>
																			<div class=""company"">Vormetric</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Head of Product Management</strong></p>
<p>Derek Tumulak (<a title=""Follow Derek Tumulak on Twitter"" href=""https://twitter.com/Tumulak"" target=""_blank"">@Tumulak</a>) joined the company in June 2012 because he felt Vormetric was the best place for him to leverage the product management and engineering expertise he’d amassed over 15 years in the information security industry. As head of the product management organization, Tumulak is responsible for product direction and strategy, working closely with Vormetric’s enterprise, government and cloud service provider customers to develop and deliver products that meet their data security needs in the face of a rapidly intensifying threat landscape and the corporate mandate to leverage cloud technologies.</p>
<p>Tumulak has deep domain expertise, having spent three years as vice president of product management for enterprise data protection at SafeNet. He also served as vice president of product management and engineering for Ingrian (acquired by SafeNet) where he helped grow the company from an early-stage startup to a leading provider of data security and compliance solutions for enterprises, financial services, retail businesses and healthcare organizations. Tumulak holds a bachelor’s degree in computer engineering from the University of Waterloo in Canada.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-327"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/bWiederhold.Headshot.WEB_-e1410455062316-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-327"">Bob Wiederhold</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-327"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/bWiederhold.Headshot.WEB_-e1410455062316-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Bob Wiederhold</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>CEO </strong></p>
<p>Bob has more than 25 years of high technology experience. Until an acquisition by IBM in 2008, Bob served as chairman, CEO, and president of Transitive Corporation, the worldwide leader in cross-platform virtualization with over 20 million users. Previously, he was president and CEO of Tality Corporation, the worldwide leader in electronic design services, whose revenues and size grew to almost $200 million and had 1,500 worldwide employees. Bob held several executive general management positions at Cadence Design Systems, Inc., an electronic design automation company, which he joined in 1985 as an early stage start-up and helped to grow to more than $1.5 billion during his 13 years at the company. Bob also headed High Level Design Systems, a successful electronic design automation start-up that was acquired by Cadence in 1996. Bob has extensive board experience having served on both public (Certicom, HLDS) and private company boards (Snaketech, Tality, Transitive, FanfareGroup).</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/bobwiederhold"">@bobwiederhold</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-111"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/alfke-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-111"">Jens Alfke</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-111"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/alfke-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Jens Alfke</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Mobile Engineering Peer</strong></p>
<p>Jens Alfke is a Mobile Engineering Peer at Couchbase, leading the development of Couchbase Mobile for iOS. He has a long history as a Mac developer and spent 15 years at Infinite Loop working on everything from AppleScript and Stickies to iChat and Safari RSS, followed by two years at Google and RockMelt extending the Chrome browser. He&#8217;s an avid proponent of decentralized social software, despite never having managed to release any.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/snej"">@snej</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-98"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Biyikoglu-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-98"">Cihan Biyikoglu</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-98"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Biyikoglu-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Cihan Biyikoglu</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Director of Product Management</strong></p>
<p>Cihan Biyikoglu is the Director of Product Management at Couchbase. Before joining Couchbase, Cihan worked on a number of products including Twitter, Azure and SQL Server platforms at Microsoft and HealthVault health data platform under Microsoft Research. Previously, Cihan Biyikoglu also worked on database technologies such as Illustra and Informix Dynamic Server at Informix. He has a master’s degree in Database Systems from University of Westminster in the UK and a Computer Engineering degree from Yildiz Technical University in Turkey.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-135"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/wcarter-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-135"">Wayne Carter</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-135"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/wcarter-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Wayne Carter</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong> Chief Architect, Mobile</strong></p>
<p>Wayne is the Chief Architect of Mobile at Couchbase, where he is responsible for leading vision, strategy and development for the company’s mobile solutions. Before Couchbase, Wayne spent 7 years at Oracle as the Architect responsible for driving mobile innovation within the CRM and SAAS product lines. He has 11 patents pending from his work at Oracle. Prior to Oracle, Wayne held technical leadership positions at Siebel working on their CRM product line.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/waynecarter"">@waynecarter</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-414"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-414"">Laurent Dougin</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-414"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Laurent Dougin</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Developer Advocate </strong></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-128"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/fehre-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-128"">Philipp Fehre</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-128"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/fehre-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Philipp Fehre</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Developer Advocate</strong></p>
<p>Philipp Fehre is a Developer Advocate at Couchbase working with the community to improve the Couchbase experience all over. He has experience in network technologies, Operating Systems and development and scaling of Web APIs. He studied at the Technical University of Munich working on Virtual Machine Live Migration and datacenter network monitoring.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/ischi"">@ischi</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-137"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sgarma-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-137"">Zack Gramana</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-137"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sgarma-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Zack Gramana</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Senior Software Engineer, Mobile</strong></p>
<p>Zack Gramana is the creator and maintainer of Couchbase Lite for .NET. He kicked off the project while working at Xamarin, and now continues his work on Couchbase’s mobile team. He’s been programming since he was a little kid staring into the glow of his Commodore 64, and is nerdy for things like distributed, and embedded, computing.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/zgramana"">@zgramana</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-118"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/ming-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-118"">Matt Ingenthron</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-118"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/ming-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Matt Ingenthron</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Director of Developer Advocates</strong></p>
<p>Matt Ingenthron is the Director of Developer Advocates at Couchbase and an experienced web architect with a software development background. He has deep expertise in building, scaling and operating global-scale Java, Ruby on Rails and AMP web applications. He has been a contributor to the memcached project, one of the maintainers of the Java spymemcached client and a core developer on Couchbase. He is currently heading up Couchbase&#8217;s work in getting the right bits needed for PHP, Java, .NET and Ruby developers.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/ingenthr"">@ingenthr</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-130"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sjohnson-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-130"">Shane Johnson</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-130"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sjohnson-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Shane Johnson</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Manager of Product Marketing</strong></p>
<p>Shane K Johnson is the Manager of Product Marketing at Couchbase. Prior to Couchbase, he occupied various roles in developing and evangelism with a background in Java and distributed systems. He has consulted with organizations in the financial, retail, telecommunications, and media industries to draft and implement architectures that relied on distributed systems for data and analysis.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/shane_dev"">@shane_dev</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-115"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Kirk_Kirkconnell-400x400.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-115"">Kirk Kirkconnell</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-115"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Kirk_Kirkconnell-400x400.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Kirk Kirkconnell</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Senior Solutions Engineer</strong></p>
<p>Kirk Kirkconnell is a Senior Solutions Engineer at Couchbase working with customers in multiple capacities to assist them in architecting, deploying and managing Couchbase. His expertise is in operations, hosting and support of large-scale application and database infrastructures.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-126"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/krug-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-126"">Perry Krug</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-126"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/krug-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Perry Krug</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Solutions Architect</strong></p>
<p>Perry Krug is a Solutions Architect at Couchbase working with customers in all capacities to aid in their experiences with Couchbase. He has an expertise in networking, enterprise storage, Web 2.0 infrastructure and NoSQL databases.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/perrykrug"">@perrykrug</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-96"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/ak-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-96"">Anil Kumar</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-96"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/ak-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Anil Kumar</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Product Manager, Server</strong></p>
<p>As a Product Manager at Couchbase, Anil Kumar is responsible for all Couchbase Server product development, roadmap, positioning, messaging and collateral. Prior to joining Couchbase, Anil spent several years working at Microsoft in the Entertainment division and most recently in the Windows and Windows Live division. Anil holds a Masters degree in Computer Science and a BS in Information Technology.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-233"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sarath-e1409961115310.jpeg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-233"">Sarath Lakshman</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-233"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/sarath-e1409961115310.jpeg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Sarath Lakshman</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Engineer</strong></p>
<p><span data-sheets-value=""[null,2,&quot;Sarath Lakshman (@t3rm1n4l) is a Software Engineer at Couchbase. Sarath is a core developer for Couchbase MapReduce View Engine with a focus on improving performance. Before Couchbase, he worked at Zynga for over two years building ZBase, a distributed storage platform that powered all of the social games infrastructure at Zynga. Sarath was bitten by Linux in his early days of teenage and he created a Linux distribution called Slynux . He is also the author of Linux shell scripting cookbook. Sarath holds a BS degree in Computer Science from Model Engineering College, India.&quot;]"" data-sheets-userformat=""[null,null,897,[null,0],null,null,null,null,null,null,0,1,0]"">Sarath Lakshman (@t3rm1n4l) is a Software Engineer at Couchbase. Sarath is a core developer for Couchbase MapReduce View Engine with a focus on improving performance. Before Couchbase, he worked at Zynga for over two years building ZBase, a distributed storage platform that powered all of the social games infrastructure at Zynga. Sarath was bitten by Linux in his early days of teenage and he created a Linux distribution called Slynux . He is also the author of Linux shell scripting cookbook. Sarath holds a BS degree in Computer Science from Model Engineering College, India.</span></p>
															<a class=""speaker-twitter"" href=""https://twitter.com/t3rm1n4l"">@t3rm1n4l</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-134"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-134"">Traun Leyden</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-134"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/speaker_400x400-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Traun Leyden</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Senior Software Engineer </strong></p>
<p>Traun Leyden is a Senior Software Engineer at Couchbase currently working on the Touch-DB/Android project. Prior to Couchbase, Traun co-founded Signature Labs, a venture backed startup that developed an iOS mobile CRM geolocation app that was deployed in Fortune 500 companies. Among his accomplishments, Traun developed a highly popular Android ringer control app called &#8220;Buzzoff&#8221; and created an open source Neural Network library written in Go that leverages Go channels/goroutines to achieve high computation concurrency.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/tleyden"">@tleyden</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-239"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/22639b8.jpg')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-239"">Alex Ma</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-239"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/22639b8.jpg')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Alex Ma</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><span style=""font-size: 13px; font-family: arial,sans,sans-serif;"" data-sheets-value=""[null,2,&quot;Alex Ma is a Principal Solutions Engineer at Couchbase and responsible for helping enterprises understand how they can benefit from NoSQL and providing guidance as they go to production.  His expertise is in operations, monitoring, automation and scaling production environments.&quot;]"" data-sheets-userformat=""[null,null,769,[null,0],null,null,null,null,null,null,null,1,0]"">Alex Ma is a Principal Solutions Engineer at Couchbase and responsible for helping enterprises understand how they can benefit from NoSQL and providing guidance as they go to production. His expertise is in operations, monitoring, automation and scaling production environments.</span></p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-113"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/michaels-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-113"">Justin Michaels</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-113"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/michaels-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Justin Michaels</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Field Sales Engineer</strong></p>
<p>Justin has over 15 years of industry experience filling a variety of roles and understands the architecture challenges in deploying and maintaining mission critical systems. He is a Solution Architect at Couchbase and responsible for driving customer success, actively managing technical architecture and deployments. Filling the architect roles facilitates regular interactions with a wide variety customers providing enterprise planning and support.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-109"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/jmorris-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-109"">Jeff Morris</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-109"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/jmorris-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Jeff Morris</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Developer Advocate</strong></p>
<p>Jeff Morris is a Developer Advocate at Couchbase and a former Enterprise Architect at Source Interlink. Jeff maintains the Open Source Couchbase .NET SDK and has over 14 years’ experience developing and architecting systems under a variety of platforms and technology primarily using Microsoft solutions</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/jeffrysmorris"">@jeffrysmorris</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-122"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Michael_Nitschinger-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-122"">Michael Nitschinger</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-122"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Michael_Nitschinger-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Michael Nitschinger</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Developer Advocate</strong></p>
<p>Michael Nitschinger is a Vienna-based Developer Advocate for Couchbase. He is the lead developer of the Couchbase Java SDK, a current maintainer of the popular spymemcached library and responsible for enterprise framework integration (especially Spring-Data-Couchbase). Michael is active in the open source community, a core member of the Netty project, and also contributing to various other projects like Reactor. Prior to working at Couchbase, Michael worked in the enterprise consulting business, focusing on network performance and monitoring integration solutions.Michael is an engineer at Couchbase.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/daschl"">@daschl</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-102"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/d-pinto-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-102"">Don Pinto</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-102"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/d-pinto-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Don Pinto</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Product Manager, Server</strong></p>
<p>Don Pinto is a Product Manager at Couchbase and is currently focused on advancing the server capabilities of Couchbase Server including Security. He is extremely passionate about data technology and in the past, has authored several articles on Couchbase Server including technical blogs and whitepapers. Prior to joining Couchbase, Don spent several years at IBM where he maintained the role of software developer in the DB2 information management group and most recently as a Program Manager on the SQL Server team at Microsoft. Don holds a Masters degree in Computer Science and a Bachelors degree in Computer Engineering from the University of Toronto, Canada.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/NoSQLDon"">@NoSQLDon</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-120"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/revell-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-120"">Matthew Revell</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-120"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/revell-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Matthew Revell</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Lead Developer Advocate</strong></p>
<p>Matthew is the Lead Developer Advocate at Couchbase in EMEA, where he helps to grow the Couchbase community and works with developers to build scalable, low latency back-ends for their software projects.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/matthewrevell"">@matthewrevell</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-106"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Sangudi-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-106"">Gerald Sangudi</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-106"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Sangudi-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Gerald Sangudi</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Architect</strong></p>
<p>Gerald Sangudi leads the Query Engineering team at Couchbase, which is creating N1QL, the upcoming query language and engine for the Couchbase distributed document database. Gerald has been building enterprise software for over 20 years. He started out building data mining and visualization software at Silicon Graphics, and went on to build more software at VMware, Healtheon/WebMD, Yahoo!, Loopt, and 23andMe. He studied Computer Science and Engineering at MIT, and holds several patents.</p>
															<a class=""speaker-twitter"" href=""https://twitter.com/sangudi"">@sangudi</a>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-116"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/mschoch-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-116"">Marty Schoch</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-116"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/mschoch-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Marty Schoch</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Engineer</strong></p>
<p>Marty Schoch is a Software Engineer at Couchbase. Marty is the author of the Couchbase Plug-in for Elasticsearch and early versions of N1QL. Marty is also a core contributor to the Couchbase Go SDK and has worked on many experimental Couchbase Labs projects using go. Currently Marty is researching new index technology for future versions of Couchbase. Marty holds a BS in Computer Science from the University of Maryland, College Park.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-97"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Chiyoung_Seo-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-97"">Chiyoung Seo</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-97"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/Chiyoung_Seo-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Chiyoung Seo</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Architect</strong></p>
<p>Chiyoung Seo is a Software Architect at Couchbase and mainly works on caching, replication, and storage engines. His recent focus is to develop the next generation of a storage engine that is optimized for Solid State Drives (SSD). Before Couchbase, he worked at Yahoo! for two years and participated in developing the next generation of display advertising platform. Chiyoung received his Ph.D. degree from Computer Science Department at University of Southern California in 2008 and published dozens of papers in the area of software architecture, distributed systems, and database.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-124"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/mweiderhold-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-124"">Mike Wiederhold</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-124"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/mweiderhold-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Mike Wiederhold</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Software Engineer</strong></p>
<p>Mike Wiederhold is a Software Engineer at Couchbase. Mike has worked on performance evaluation of Couchbase and is the author of the Couchbase-Hadoop plugin, as well as a core contributor to Spymemcached and the Couchbase Java SDK. He currently is focused on Couchbase&#8217;s persistence, replication, and caching tiers. Mike holds a BS in Computer Science with an emphasis in Computer Networking and Distributed Systems from the University of California, Santa Barbara.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
								<div class=""speaker-wrap"">
				<div class=""headshot-outer"">
					<a href=""#speaker-132"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/syen-400x400.png')""></div>
					</a>
				</div>
				<div class=""speaker-info"">
					<h3><a href=""#speaker-132"">Steve Yen</a></h3>
																						<div class=""company"">Couchbase</div>
									</div>
			</div><!-- .speaker-wrap -->
			<div id=""speaker-132"" style=""display: none; max-width: 100%;"">
				<div class=""speaker-wrap speaker-pop"" style=""display: block; width: auto;"">
					<div class=""headshot-outer"">
						<div class=""headshot"" style=""background-image: url('http://www.couchbase.com/connect/wp-content/uploads/2014/09/syen-400x400.png')""></div>
					</div>
					<div class=""speaker-info"">
						<h3>Steve Yen</h3>
																			<div class=""company"">Couchbase</div>
												<div class=""section"" style=""padding-bottom: 0px;"">
							<p><strong>Co-Founder</strong></p>
<p>Steve Yen is a world-class technologist and successful entrepreneur with a track record of founding and building groundbreaking companies. Prior to Couchbase, Steve co-founded Escalate, and architected its unique, multitenant e-commerce and order management software-as-a-service application. Previously, Steve co-founded Kiva Software, where he was a key part of the technology team that created the world’s first application server with high-scale deployments at E*Trade, Bank of America and Sabre. Steve holds a BS in Computer Science from the University of California, Berkeley.</p>
													</div><!-- .section -->
					</div>
				</div><!-- .speaker-wrap -->
			</div>
			</div>
	<script type=""text/javascript"">
		jQuery( document ).ready( function( e ) {
			if ( jQuery( window ).width() > 450 ) {
				jQuery( '.speaker-info h3 a, .headshot-outer a' ).fancybox( {
					maxWidth	: 450,
					fitToView	: false,
					autoSize	: false,
					closeClick	: false,
					autoCenter	: false,
					helpers		: { overlay : { locked : false } }
				} );
			} else {
				jQuery( '.speaker-info h3 a, .headshot-outer a' ).fancybox( {
					maxWidth	: 320,
					fitToView	: true,
					autoSize	: false,
					closeClick	: false,
					autoCenter	: false,
					helpers		: { overlay : { locked : false } }
				} );
			}
		} );
	</script>
				</div><!-- .entry-content -->
			</article><!-- #post-## -->
		</div><!-- .section -->


</div><!-- #main-wrapper -->

<footer>
		<div class=""container"">
				<div class=""row-fluid"">
						<div class=""span4"">
								
						</div>
						<div class=""span4"">
								
						</div>
						<div class=""span4"">
								
						</div>
				</div>
		</div>
		<div class=""container"">
								<p>© 2014 COUCHBASE All rights reserved.  <a href=""http://www.couchbase.com"" target=""_blank"">Couchbase.com</a> </p>
		</div>
		<!-- /container --> 
</footer>

<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/navigation.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/scripts.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/prettify.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/bootstrap.min.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/fancybox/jquery.fancybox.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/jquery.sticky.js?ver=20120206'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-content/themes/couchbase/js/skip-link-focus-fix.js?ver=20130115'></script>
<script type='text/javascript' src='http://www.couchbase.com/connect/wp-includes/js/comment-reply.min.js?ver=4.0'></script>

	<script type=""text/javascript"">
		jQuery( document ).ready( function( e ) {
			if ( jQuery( window ).width() > 979 ) {
				jQuery( document ).scrollTop( jQuery( '#carousel-footer' ).offset().top - 100 );
			} else {
				jQuery( document ).scrollTop( jQuery( '#carousel-footer' ).offset().top );
			}
		} );
	</script>

</body>
</html>
";

		}

	}
}

