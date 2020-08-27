import firebase from 'firebase/app'
import 'firebase/firestore'
import 'firebase/auth'

const config = {
    apiKey: "AIzaSyAmQvFLSB7Y_TfulO3k-kGybc4tgUJzvMA",
    authDomain: "rentagym.firebaseapp.com",
    databaseURL: "https://rentagym.firebaseio.com",
    projectId: "rentagym",
    storageBucket: "rentagym.appspot.com",
    messagingSenderId: "22825725573",
    appId: "1:22825725573:web:39412b04e113161f8d43ec",
    measurementId: "G-63QQ35NHZX"
  };

  export const createUserProfileDocument = async (userAuth, additionalData) => 
  {
    if(!userAuth) return;

    const userRef = firestore.doc(`users/${userAuth.uid}`);
    const snapShot = await userRef.get();
    if(!snapShot.exists)
    {
      const { displayName, email} = userAuth;
      const createdAt = new Date();

      try
      {
        await userRef.set({
          displayName,
          email,
          createdAt,
          ...additionalData
        })
      }
      catch(err)
      {
        console.log('error creating user, error.message');
      }
    }

    return userRef;
  }

  firebase.initializeApp(config);

  export const auth = firebase.auth();
  export const firestore = firebase.firestore();

  const provider = new firebase.auth.GoogleAuthProvider();
  provider.setCustomParameters({ prompt: 'select_account'});
  export const signInWithGoogle = () => auth.signInWithPopup(provider);

  export default firebase;