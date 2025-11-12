import { View, Text, SafeAreaView, StatusBar, TouchableOpacity, TextInput, Pressable, ToastAndroid,  } from 'react-native'
import React, { useState } from 'react'
import MaterialIcons from '@react-native-vector-icons/material-icons'
import { ipAddress } from '../../config/ipAddress'
import { useAuth } from '../context/Auth'
export default function LoginScreen({ navigation, route }) {
    const [email, setEmail] = useState('qaismuhammad74902@gmail.com')
    const [password, setpassword] = useState('12345678')
    const { login } = useAuth();
    const handleLogin = async () => {
        try {
            const response = await fetch(`${ipAddress}/api/Auth/LoginCustomer`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    Email: email,
                    Password: password,
                }),
            });

            const data = await response.json(); // directly parse JSON

            if (response.ok) {
                ToastAndroid.show('Login Successful', ToastAndroid.SHORT);
                console.log("Login response:", data);
                // If you want to store token or navigate
                await login(data.data);
                navigation.navigate('homeScreen');
            } else {
                ToastAndroid.show(data.message || 'Login failed', ToastAndroid.SHORT);
                console.log("Login failed:", data);
            }
        } catch (error) {
            console.error("Error during login:", error);
            ToastAndroid.show('An error occurred', ToastAndroid.SHORT);
        }
    };


    return (
        <SafeAreaView style={{ flex: 1, marginTop: StatusBar.currentHeight || 0, backgroundColor: '#ffff' }}>
            <View style={{ padding: 15, backgroundColor: '#2f2f91ff', flexDirection: 'row', alignItems: 'center' }}>
                <TouchableOpacity onPress={() => navigation.goBack()}>
                    <MaterialIcons name='arrow-back' size={25} color={'#dddd'} />
                </TouchableOpacity>
                <Text style={{ fontSize: 20, marginLeft: 10, letterSpacing: 2, color: '#dddd', textAlign: 'center' }}>Login</Text>
            </View>
            <View style={{ justifyContent: 'center', alignItems: 'center', marginTop: 100, marginBottom: 30 }}>
                <MaterialIcons name='shopping-cart' size={150} color={'#0d1385ff'} />
            </View>
            <View style={{ backgroundColor: '#ffff' }}>
                <TextInput
                    placeholder='Email'
                    style={{
                        margin: 10,
                        borderRadius: 100, borderWidth: 1, width: '90%',
                        justifyContent: 'center', alignSelf: 'center', padding: 13
                    }}
                    keyboardType='email-address'
                    value={email}
                    onChangeText={setEmail}
                />
                <TextInput
                    secureTextEntry
                    placeholder='Password'
                    style={{
                        margin: 10,
                        borderRadius: 100, borderWidth: 1, width: '90%',
                        justifyContent: 'center', alignSelf: 'center', padding: 13
                    }}
                    keyboardType='visible-password'
                    value={password}
                    onChangeText={setpassword}
                />
            </View>
            <View style={{ flex: 0.6 }}>
                <Pressable onPress={handleLogin}>
                    <View style={{ backgroundColor: '#162d71ff', width: '80%', elevation: 10, borderRadius: 100, justifyContent: 'center', alignSelf: 'center', margin: 30 }}>
                        <Text style={{ padding: 10, color: '#ddddddff', letterSpacing: 2, fontSize: 20, textAlign: 'center' }}>
                            Login
                        </Text>
                    </View>
                </Pressable>
                <Pressable onPress={() => navigation.navigate('ForgotPassword')}>
                    <Text style={{ padding: 10, color: '#091d79ff', letterSpacing: 2, fontSize: 20, textAlign: 'center' }}>
                        Forgot Your Password?
                    </Text>
                </Pressable>
                <Pressable onPress={() => navigation.navigate('SignUpScreen')}>
                    <Text style={{ padding: 10, color: '#091d79ff', fontSize: 20, textAlign: 'center' }}>
                        Don't Have An Account? SignUp
                    </Text>
                </Pressable>
            </View>
        </SafeAreaView>
    )
}