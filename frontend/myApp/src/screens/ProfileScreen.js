import { View, Text, SafeAreaView, StatusBar, Pressable, StyleSheet, ScrollView, Image } from 'react-native'
import React from 'react'
import { useAuth } from '../context/Auth'
export default function ProfileScreen({ navigation, route }) {
  const { user, login, logout } = useAuth()
  console.log(user)

  let loginAuthentication = () => {
    navigation.navigate('LoginScreen')
  }
  let signupAuthentication = () => navigation.navigate('SignUpScreen')
  if (!user) {
    return (
      <SafeAreaView style={{ flex: 0.8, padding: 10, marginTop: StatusBar.currentHeight - 10 || 0 }}>
        <View style={{ alignItems: 'center', justifyContent: 'center', flex: 1 }}>
          <Text style={{ fontSize: 18 }}>Please login to view your profile.</Text>
        </View>
        <Pressable onPress={signupAuthentication} >
          <View style={{ marginTop: 20, backgroundColor: '#203ba7ff', padding: 10, borderRadius: 5, alignItems: 'center' }}>
            <Text style={{ color: '#eee8e8dd', fontSize: 20, fontWeight: '900' }}>Sign Up</Text>
          </View>
        </Pressable>
        <Pressable onPress={loginAuthentication} >
          <View style={{ marginTop: 20, backgroundColor: '#203ba7ff', padding: 10, borderRadius: 5, alignItems: 'center' }}>
            <Text onPress={loginAuthentication} style={{ color: '#eee8e8dd', fontSize: 20, fontWeight: '900' }}>Log In</Text>
          </View>
        </Pressable>
      </SafeAreaView>
    )
  }
  const profileImageUri = user.image
    ? `data:image/jpeg;base64,${user.image}`
    : null;

  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: '#f5f5f5', paddingTop: StatusBar.currentHeight || 0 }}>
      <ScrollView contentContainerStyle={styles.container}>
        <View style={styles.card}>
          {/* Profile Image */}
          <View style={styles.imageContainer}>
            {profileImageUri ? (
              <Image source={{ uri: profileImageUri }} style={styles.profileImage} />
            ) : (
              <View style={styles.placeholderImage}>
                <Text style={{ color: '#fff', fontSize: 18 }}>No Image</Text>
              </View>
            )}
          </View>

          {/* User Details */}
          <View style={styles.infoContainer}>
            <View>
              <Text style={styles.name}>{user.full_name}</Text>
              <Text style={styles.email}>{user.email}</Text>
              <Text style={styles.phone}>{user.phonenumber}</Text>
            </View>
          </View>
        </View>
        <Pressable style={{ width: '90%', margin: 10 }} onPress={() => {
          logout()
        }}>
          <View style={{ backgroundColor: '#d73030dd', padding: 10, borderRadius: 10 }}>
            <Text style={{ fontSize: 20, textAlign: 'center', color: '#ebe308dd', letterSpacing: 2, fontWeight: '900' }}>Logout</Text>
          </View>
        </Pressable>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    paddingVertical: 20,
  },
  card: {
    flexDirection: 'row',
    width: '95%',
    backgroundColor: '#fff',
    borderRadius: 15,
    padding: 20,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 5,
  },
  imageContainer: {
    marginBottom: 20,
  },
  profileImage: {
    width: 80,
    height: 80,
    borderRadius: 60,
  },
  placeholderImage: {
    width: 120,
    height: 120,
    borderRadius: 60,
    backgroundColor: '#9e9797',
    justifyContent: 'center',
    alignItems: 'center',
  },
  infoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  name: {
    fontSize: 22,
    marginLeft: 10,
    fontWeight: '600',
    marginBottom: 5,
  },
  email: {
    fontSize: 15,
    marginLeft: 10,
    color: '#555',
    marginBottom: 3,
  },
  phone: {
    fontSize: 16,
    marginLeft: 10,
    color: '#555',
  },
});