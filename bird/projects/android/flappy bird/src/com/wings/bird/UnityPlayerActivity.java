package com.wings.bird;

import net.youmi.android.AdManager;
import net.youmi.android.banner.AdSize;
import net.youmi.android.banner.AdView;
import net.youmi.android.banner.AdViewListener;
import net.youmi.android.spot.SpotManager;

import com.unity3d.player.*;

import android.app.Activity;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.LinearLayout;

public class UnityPlayerActivity extends Activity {
	protected static final String TAG = "UnityPlayerActivity";
	protected UnityPlayer mUnityPlayer; // don't change the name of this
	private AdView adView;
	// variable; referenced from native code
	static UnityPlayerActivity currentActivity;

	private Handler handler;

	public static int showCounter = 0;
	public static int spotShowLimit = 10;

	// Setup activity layout
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		setTheme(android.R.style.Theme_NoTitleBar_Fullscreen);
		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia
														// play happy

		mUnityPlayer = new UnityPlayer(this);
		if (mUnityPlayer.getSettings().getBoolean("hide_status_bar", true))
			getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
					WindowManager.LayoutParams.FLAG_FULLSCREEN);

		setContentView(mUnityPlayer);
		mUnityPlayer.requestFocus();

		SpotManager.getInstance(this).loadSpotAds();
		SpotManager.getInstance(this).setSpotOrientation(
				SpotManager.ORIENTATION_PORTRAIT);

		AdManager.getInstance(this).init("629422a09d7597f5",
				"3882476ea4b4d907", false);

		currentActivity = this;

		handler = new Handler() {
			public void handleMessage(Message msg) {
				switch (msg.what) {
				case 1:
					newAd();
					break;
				case 2:
					wall();
					break;
				case 0:
				default:
					if (adView == null) {
						Log.d(TAG, "adView null");
						return;
					}
					adView.setVisibility(View.INVISIBLE);
					adView = null;
					break;
				}
			}
		};
	}

	// Quit Unity
	@Override
	protected void onDestroy() {
		mUnityPlayer.quit();
		super.onDestroy();
	}

	// Pause Unity
	@Override
	protected void onPause() {
		super.onPause();
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override
	protected void onResume() {
		super.onResume();
		mUnityPlayer.resume();
	}

	// This ensures the layout will be correct.
	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}

	// Notify Unity of the focus change.
	@Override
	public void onWindowFocusChanged(boolean hasFocus) {
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
	}

	// For some reason the multiple keyevent type is not supported by the ndk.
	// Force event injection by overriding dispatchKeyEvent().
	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
			return mUnityPlayer.injectEvent(event);
		return super.dispatchKeyEvent(event);
	}

	// Pass any events not handled by (unfocused) views straight to UnityPlayer
	@Override
	public boolean onKeyUp(int keyCode, KeyEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	/* API12 */public boolean onGenericMotionEvent(MotionEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	void open() {
		show = true;

		if (showCounter < spotShowLimit) {
			handler.sendEmptyMessage(1);
		} else {
			showCounter = 0;
			handler.sendEmptyMessage(2);
		}

		showCounter++;
	}

	void close() {
		show = false;
		handler.sendEmptyMessage(0);
	}

	private boolean show = false;

	void toggle() {
		if (show) {
			handler.sendEmptyMessage(2);
		} else {
			handler.sendEmptyMessage(1);
		}
		show = !show;

	}

	void wall() {
		SpotManager.getInstance(this).showSpotAds(this);
	}

	void newAd() {

		FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(
				FrameLayout.LayoutParams.MATCH_PARENT,
				FrameLayout.LayoutParams.WRAP_CONTENT);
		// 设置广告条的悬浮位置
		layoutParams.gravity = Gravity.TOP | Gravity.LEFT; // 这里示例为右下角

		// 实例化广告条
		adView = new AdView(this, AdSize.FIT_SCREEN);

		adView.setAdListener(new AdViewListener() {

			@Override
			public void onSwitchedAd(AdView adView) {
				// 切换广告并展示
				Log.d("adView", "onSwitchedAd");
			}

			@Override
			public void onReceivedAd(AdView adView) {
				// 请求广告成功
				Log.d("adView", "onReceivedAd");
			}

			@Override
			public void onFailedToReceivedAd(AdView adView) {
				// 请求广告失败
				Log.d("adView", "onFailedToReceivedAd");
			}
		});
		// 调用 Activity 的 addContentView 函数
		this.addContentView(adView, layoutParams);

	}
}
