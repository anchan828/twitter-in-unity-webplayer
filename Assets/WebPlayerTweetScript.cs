using UnityEngine;
using System.Collections;

public class WebPlayerTweetScript : MonoBehaviour
{
	
	//現在シェアツイートボックスをポップアップで出してつぶやくのと
	//Unity内でつぶやく2種類を実装しています
	//うまく使い分けてください
	
	//ツイート内容
	public string text = "";
	//URL短縮後の文字数でカウントしてくれる
	public string url = "";
	//（@~さんから)と表示される
	public string via = "";
	//ハッシュタグ
	public string hashtag = "";

	private string SHAREURL = "http://twitter.com/share?";
	//Twitterアプリのコンシューマーキー。Unity内でつぶやかせたいときに使用する。
	public string consumerKey = "";
	
	//アクセストークンを取得する。ポップアップで認証画面が表示される。TweetPostの前に呼び出す。最初の一回だけ呼び出せばいい。
	public bool Connect ()
	{
		Application.ExternalEval ("!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src='http://code.jquery.com/jquery-1.7.1.min.js';fjs.parentNode.insertBefore(js,fjs)}}(document,'script','jQuery');");
		Application.ExternalEval ("!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src='http://platform.twitter.com/anywhere.js?id=" + consumerKey + "&v=1';fjs.parentNode.insertBefore(js,fjs)}}(document,'script','anywhere');");
		Application.ExternalEval ("var url='https://oauth.twitter.com/2/authorize?oauth_callback_url='+encodeURIComponent(location.href)+'&oauth_mode=flow_web_client&oauth_client_identifier=" + consumerKey + "';var F=0;if(screen.height>500){F=Math.round((screen.height/2)-(250))}window.open(url,'twitter_anywhere_auth','left='+Math.round((screen.width/2)-(250))+',top='+F+',width=500,height=500,personalbar=no,toolbar=no,resizable=no,scrollbars=yes')");
		return true;
	}
	//つぶやく。Unity側に反応は帰ってこないです。
	public void TweetPost (string t)
	{
		Application.ExternalEval ("$.ajax({type:'post',url:'https://api.twitter.com/1/statuses/update.json?status=" + WWW.EscapeURL (t) + "&oauth_access_token='+encodeURIComponent(localStorage.getItem('twttr_anywhere'))});");
	}
	//シェアツイートボックスをポップアップで表示。
	public void ShareTweet ()
	{
		string s = SHAREURL + "original_referer=&text=" + WWW.EscapeURL (text) + "&url=" + WWW.EscapeURL (url) + "&hashtags=" + hashtag;
		if (via.Length != 0)
			s += "&via=" + WWW.EscapeURL (via);
		Application.ExternalEval ("var F = 0;if (screen.height > 500) {F = Math.round((screen.height / 2) - (250));}window.open('" + s + "','intent','left='+Math.round((screen.width/2)-(250))+',top='+F+',width=500,height=260,personalbar=no,toolbar=no,resizable=no,scrollbars=yes');");
	}
}
