import { View, Text, SafeAreaView, StatusBar } from 'react-native'
import React from 'react'

export default function ProfileScreen() {
  return (
    <SafeAreaView style={{ flex: 1, padding: 10, marginTop: StatusBar.currentHeight - 10 || 0 }}>
      <Text>Profile</Text>
    </SafeAreaView>
  )
}