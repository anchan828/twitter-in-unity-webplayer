using UnityEngine;
using System.Collections;

public class SampleScript : MonoBehaviour
{
	private WebPlayerTweetScript wpt;
	private bool isConnect = false;
	public string tweet = "Tweet";
	void Start ()
	{
		wpt = GetComponent<WebPlayerTweetScript> ();
	}

	void OnGUI ()
	{
		GUI.Window (0, new Rect (0, 0, Screen.width / 2, 200), TweetWindow, "ツイートウィンドウ");
		GUI.Window (1, new Rect (Screen.width / 2, 0, Screen.width / 2, 200), ShareWindow, "シェアウィンドウ");
	}

	void ShareWindow (int id)
	{
		wpt.text = CreateTextField ("ツイート", wpt.text);
		wpt.url = CreateTextField ("URL", wpt.url);
		wpt.hashtag = CreateTextField ("ハッシュタグ", wpt.hashtag);
		wpt.via = CreateTextField ("via", wpt.via);
		GUILayout.Space (20);
		if (GUILayout.Button ("シェアツイート画面の作成"))
			wpt.ShareTweet ();
	}

	string CreateTextField (string label, string text)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label (label);
		GUILayout.Space (5);
		text = GUILayout.TextField (text, GUILayout.Width (Screen.width / 2 - 100));
		GUILayout.EndHorizontal ();
		return text;
	}

	void TweetWindow (int id)
	{
		if (!isConnect) {
			if (GUILayout.Button ("接続")) {
				isConnect = wpt.Connect ();
			}
			GUILayout.Label("Twitterアプリの登録が必要となります");
		} else {
			GUILayout.Space (10);
			//入力位置が三文字目以降移動できないバグ？があったので回避。
			tweet = GUI.TextField (new Rect (0, 0, 0, 0), tweet);
			tweet = GUILayout.TextField (tweet);
			if (GUILayout.Button ("ツイート")) {
				wpt.TweetPost (tweet);
				tweet = "";
			}
		}
	}
}
