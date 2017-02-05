package arzombie.locationservices;

import android.Manifest;
import android.content.Intent;
import android.content.IntentSender;

import java.util.*;

import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import android.R;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;


public class getLocation extends FragmentActivity implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

	private GoogleApiClient mGoogleApiClient;
	public static final String TAG = getLocation.class.getSimpleName();
	private GoogleMap mMap;
	private final static int CONNECTION_FAILURE_RESOLUTION_REQUEST = 9000;
	private LocationManager locationManager;


	//This is readily being processed
	public double[] handleNewLocation(Location location) {
		//Log.d(TAG, location.toString());
		double locLatitude = location.getLatitude();
		double locLongitude = location.getLongitude();

		double[] output = {locLatitude, locLongitude};

		return output;

	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		startService(new Intent(this, MyService.class));

		super.onCreate(savedInstanceState);
		// Obtain the SupportMapFragment and get notified when the map is ready to be used.
		mGoogleApiClient = new GoogleApiClient.Builder(this)
				.addConnectionCallbacks(this)
				.addOnConnectionFailedListener(this)
				.addApi(LocationServices.API)
				.build();
			if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
				// TODO: Consider calling
				//    ActivityCompat#requestPermissions
				// here to request the missing permissions, and then overriding
				//   public void onRequestPermissionsResult(int requestCode, String[] permissions,
				//                                          int[] grantResults)
				// to handle the case where the user grants the permission. See the documentation
				// for ActivityCompat#requestPermissions for more details.
				return;
			}
			double[] coor = handleNewLocation(LocationServices.FusedLocationApi.getLastLocation(mGoogleApiClient));
			for (int i =0; i < 2; i++) {
				double[] input = areaDefiner(coor);
				double lat = input[0];
				double lon = input[1];

				double[] inputMe = handleNewLocation(LocationServices.FusedLocationApi.getLastLocation(mGoogleApiClient));
				double latMe = inputMe[0];
				double lonMe = inputMe[1];

				double distance = Distance(latMe, lonMe, lat, lon);


			}
//			onMapReady();

	}

	public static double Distance (double x1, double y1, double x2, double y2) {
		double distance = Math.sqrt(Math.pow((x1-x2),2) + Math.pow((y1-y2),2));
		return distance;
	}

	//This is done three times based on original location
	public static double [] areaDefiner(double [] input) { //input coordinates
		double lat = input[0];
		double lon = input[1];
		//Creates an area
		double size = 135.5;
		double poop =  Math.random() * size;
		double pm = Math.random();
		if(pm >= 0.5) {
			poop *= - 1;
		}

		double outLat = lat + poop;
		double outLon = lon + poop;

		double [] output = {outLat, outLon};
		return output;
	}

	@Override
	protected void onResume() {
		super.onResume();
		//setUpMapIfNeeded();
		mGoogleApiClient.connect();
	}

	@Override
	protected void onPause() {
		super.onPause();
		if (mGoogleApiClient.isConnected()) {
			mGoogleApiClient.disconnect();
		}
	}

	/**
	 * Manipulates the map once available.
	 * This callback is triggered when the map is ready to be used.
	 * This is where we can add markers or lines, add listeners or move the camera. In this case,
	 * we just add a marker near Sydney, Australia.
	 * If Google Play services is not installed on the device, the user will be prompted to install
	 * it inside the SupportMapFragment. This method will only be triggered once the user has
	 * installed Google Play services and returned to the app.
	 */
//	@Override
//	public void onMapReady(GoogleMap googleMap) {
//		mMap = googleMap;
//
//		// Add a marker in Sydney and move the camera
//		LatLng sydney = new LatLng(-34, 151);
//		mMap.addMarker(new MarkerOptions().position(sydney).title("Marker in Sydney"));
//		mMap.moveCamera(CameraUpdateFactory.newLatLng(sydney));
//	}

	@Override
	public void onConnected(@Nullable Bundle bundle) {
		Log.i(TAG, "Location services connected.");
		try {

			Location location = LocationServices.FusedLocationApi.getLastLocation(mGoogleApiClient);

			if (location == null){}
			else{
				handleNewLocation(location);
				areaDefiner(handleNewLocation(location));
				if(Distance(handleNewLocation(location)[0], areaDefiner(handleNewLocation(location))[0], handleNewLocation(location)[1], areaDefiner(handleNewLocation(location))[1]) < 27.1){
					//Push to unity
				}
			}

		} catch(SecurityException e) { // HAVE NO IDEA IF THIS IS RIGHT
			e.printStackTrace();
		}

	}

	@Override
	public void onConnectionSuspended(int i) {
		Log.i(TAG, "Location services suspended. Please reconnect.");
	}

	@Override
	public void onConnectionFailed(ConnectionResult connectionResult) {
		if (connectionResult.hasResolution()) {
			try {
				// Start an Activity that tries to resolve the error
				connectionResult.startResolutionForResult(this, CONNECTION_FAILURE_RESOLUTION_REQUEST);
			} catch (IntentSender.SendIntentException e) {
				e.printStackTrace();
			}
		} else {
			Log.i(TAG, "Location services connection failed with code " + connectionResult.getErrorCode());
		}
	}
}
