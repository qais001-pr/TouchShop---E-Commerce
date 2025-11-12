import {
    View, Text, SafeAreaView, StatusBar, Pressable,
    ScrollView, TextInput, Image, ToastAndroid,
    KeyboardAvoidingView, Platform
} from 'react-native';
import React, { useState } from 'react';
import MaterialIcons from '@react-native-vector-icons/material-icons';
import { launchCamera, launchImageLibrary } from 'react-native-image-picker';
import { ipAddress } from '../../config/ipAddress';

export default function SignupAuthentication({ navigation }) {
    const [profilepicture, setProfilePicture] = useState('');
    const [fullname, setFullname] = useState('Muhammad Qais');
    const [phonenumber, setPhonenumber] = useState('00923328544397');
    const [email, setEmail] = useState('qaismuhammad742@gmail.com');
    const [password, setPassword] = useState('12345678');
    const [confirmpassword, setConfirmPassword] = useState('12345678');

    const openCamera = () => {
        launchCamera(
            { maxHeight: 250, maxWidth: 250, quality: 1, mediaType: 'photo' },
            (response) => {
                if (response.didCancel || response.errorCode || response.errorMessage) return;

                const asset = response.assets?.[0];
                if (asset) {
                    setProfilePicture({
                        uri: asset.uri,
                        type: asset.type || 'image/jpeg',
                        fileName: asset.fileName || 'profile.jpg',
                    });
                } else {
                    ToastAndroid.show('Something Went Wrong!', ToastAndroid.SHORT);
                }
            }
        );
    };


    const openGallery = () => {
        launchImageLibrary(
            { maxHeight: 250, maxWidth: 250, quality: 1, mediaType: 'photo' },
            (response) => {
                if (response.didCancel || response.errorCode || response.errorMessage) return;

                const asset = response.assets?.[0];
                if (asset) {
                    // Store the entire file object needed for FormData
                    setProfilePicture({
                        uri: asset.uri,
                        type: asset.type || 'image/jpeg',
                        fileName: asset.fileName || 'profile.jpg',
                    });
                } else {
                    ToastAndroid.show('Something Went Wrong!', ToastAndroid.SHORT);
                }
            }
        );
    };


    const handleSignup = async () => {
        if (!profilepicture || !fullname || !email || !password || !confirmpassword || !phonenumber) {
            ToastAndroid.show('All fields are required!', ToastAndroid.SHORT);
            return;
        }
        if (password !== confirmpassword) {
            ToastAndroid.show('Passwords do not match!', ToastAndroid.SHORT);
            return;
        }

        try {
            const formData = new FormData();
            formData.append('FullName', fullname);
            formData.append('Email', email);
            formData.append('Password', password);
            formData.append('PhoneNumber', phonenumber);

            if (profilepicture) {
                formData.append('Image', {
                    uri: profilepicture.uri,
                    type: profilepicture.type || 'image/jpeg',
                    name: profilepicture.fileName || 'profile.jpg',
                });
            }

            const response = await fetch(`${ipAddress}/api/Auth/SignUpCustomer`, {
                method: 'POST',
                body: formData,
            });

            const data = await response.json();
            console.log(data);

            if (response.ok) {
                ToastAndroid.show('Signup Successful!', ToastAndroid.SHORT);
                navigation.navigate('LoginScreen');
            } else {
                ToastAndroid.show(data.message || 'Signup Failed', ToastAndroid.SHORT);
            }
        } catch (error) {
            console.error(error);
            ToastAndroid.show('An error occurred!', ToastAndroid.SHORT);
        }
    };


    return (
        <SafeAreaView style={{ flex: 1, marginTop: StatusBar.currentHeight || 0 }}>
            {/* Header */}
            <View style={{ flexDirection: 'row', alignItems: 'center', padding: 15, backgroundColor: '#09539dff' }}>
                <Pressable onPress={() => navigation.goBack()}>
                    <MaterialIcons name='arrow-back' size={25} color={'#ddddddff'} />
                </Pressable>
                <Text style={{ marginLeft: 10, fontSize: 20, fontWeight: '600', color: '#ddddddff', letterSpacing: 2 }}>
                    Signup
                </Text>
            </View>

            <KeyboardAvoidingView style={{ flex: 1 }} behavior={Platform.OS === 'ios' ? 'padding' : 'height'}>
                <ScrollView contentContainerStyle={{ flexGrow: 1 }} keyboardShouldPersistTaps="handled">
                    {/* Body */}
                    <View style={{ flex: 1, paddingBottom: 30 }}>
                        <View style={{ justifyContent: 'center', alignItems: 'center', margin: 20 }}>
                            <Text style={{ fontSize: 24, fontWeight: '600', color: '#09539dff' }}>Welcome to Signup</Text>
                            <Text style={{ marginTop: 10, fontSize: 16, color: '#666666ff' }}>
                                Please enter your details to create an account
                            </Text>
                        </View>

                        <View style={{ justifyContent: 'center', alignItems: 'center', margin: 5 }}>
                            {profilepicture ? (
                                <Image
                                    source={{ uri: profilepicture?.uri }}
                                    style={{ height: 150, width: 150, borderRadius: 100 }}
                                />
                            ) : (
                                <View style={{ height: 150, width: 150, backgroundColor: '#9e9797dd', justifyContent: 'center', borderRadius: 100 }}>
                                    <Text style={{ textAlign: 'center' }}>No image</Text>
                                </View>
                            )}

                            <View style={{ flexDirection: 'row', justifyContent: 'space-between', width: '40%', margin: 10 }}>
                                <Pressable onPress={openCamera}>
                                    <View style={{ backgroundColor: '#4c56adff', padding: 10, borderRadius: 5 }}>
                                        <Text style={{ fontSize: 15, color: '#ffffffdd' }}>Camera</Text>
                                    </View>
                                </Pressable>
                                <Pressable onPress={openGallery}>
                                    <View style={{ backgroundColor: '#4c56adff', padding: 10, borderRadius: 5 }}>
                                        <Text style={{ fontSize: 15, color: '#ffffffdd' }}>Gallery</Text>
                                    </View>
                                </Pressable>
                            </View>
                        </View>

                        {/* Inputs */}
                        <TextInput placeholder='Full Name' value={fullname} onChangeText={setFullname} style={styles.input} />
                        <TextInput placeholder='Email' value={email} keyboardType='email-address' onChangeText={setEmail} style={styles.input} />
                        <TextInput placeholder='Password' value={password} onChangeText={setPassword} secureTextEntry style={styles.input} />
                        <TextInput placeholder='Confirm Password' value={confirmpassword} onChangeText={setConfirmPassword} secureTextEntry style={styles.input} />
                        <TextInput placeholder='Phone Number' value={phonenumber} onChangeText={setPhonenumber} keyboardType='phone-pad' style={styles.input} />

                        {/* Buttons */}
                        <Pressable style={{ justifyContent: 'center', alignItems: 'center' }} onPress={handleSignup}>
                            <View style={{ width: '95%', backgroundColor: '#09539dff', padding: 15, borderRadius: 10, marginTop: 10 }}>
                                <Text style={{ textAlign: 'center', fontSize: 20, color: '#ddddddff', letterSpacing: 2 }}>SignUp</Text>
                            </View>
                        </Pressable>

                        <Pressable onPress={() => navigation.navigate('LoginScreen')} style={{ justifyContent: 'center', alignItems: 'center', marginTop: 10 }}>
                            <Text>
                                Have an Account? <Text style={{ color: '#1a1b85ff' }}>Login</Text>
                            </Text>
                        </Pressable>
                    </View>
                </ScrollView>
            </KeyboardAvoidingView>
        </SafeAreaView>
    );
}

const styles = {
    input: {
        borderWidth: 1,
        borderColor: '#ccc',
        margin: 10,
        paddingLeft: 15,
        paddingVertical: 10,
        borderRadius: 50,
    },
};
