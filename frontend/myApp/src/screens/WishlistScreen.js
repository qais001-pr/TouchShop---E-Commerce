import { View, Text, SafeAreaView, StatusBar, Pressable } from 'react-native'
import React from 'react'
import { useAuth } from '../context/Auth'
export default function WishlistScreen({ navigation, route }) {
  const { user } = useAuth();
  if (!user) {
    return (
      <SafeAreaView style={{ marginTop: StatusBar.currentHeight || 0, flex: 1, width: '100%' }}>
        <View style={{ backgroundColor: '#091a7cff', padding: 14 }}>
          <Text style={{ color: '#ddddddff', fontSize: 23, letterSpacing: 1 }}>
            Wishlist
          </Text>
        </View>
        <View style={{ alignItems: 'center', justifyContent: 'center', flex: 0.3 }}>
          <Text style={{ fontSize: 18 }}>Please login to view your Wishlist.</Text>
        </View>
        <View style={{ flex: 0.3, justifyContent: 'center', alignItems: 'center', width: '100%' }}>
          <Pressable onPress={() => navigation.navigate('SignUpScreen')} >
            <View style={{ marginTop: 20, backgroundColor: '#203ba7ff', padding: 10, borderRadius: 5, }}>
              <Text style={{ color: '#e3dfdfdd', fontSize: 20, fontWeight: '900' }}>Sign Up</Text>
            </View>
          </Pressable>
          <Pressable onPress={() => navigation.navigate('LoginScreen')} >
            <View style={{ marginTop: 20, backgroundColor: '#203ba7ff', padding: 10, borderRadius: 5, alignItems: 'center' }}>
              <Text style={{ color: '#eee8e8dd', fontSize: 20, fontWeight: '900' }}>Log In</Text>
            </View>
          </Pressable>
        </View>
      </SafeAreaView>
    )
  }
  return (
    <SafeAreaView style={{ flex: 1, padding: 10, marginTop: StatusBar.currentHeight - 10 || 0 }}>
      <Text>Wishlist</Text>
    </SafeAreaView>
  )
}