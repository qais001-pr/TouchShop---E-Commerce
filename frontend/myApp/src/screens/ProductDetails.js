import { View, Text, SafeAreaView, Image, StatusBar, Pressable, ScrollView } from 'react-native'
import React, { useEffect } from 'react'
import MaterialIcons from '@react-native-vector-icons/material-icons';

export default function ProductDetails({ navigation, route }) {
    const { productDetails } = route.params || {};
    console.log('Product Details:', productDetails);
    useEffect(() => {
        if (!productDetails) {
            navigation.goBack();
        }
    }, [])
    return (
        <SafeAreaView style={{ flex: 1, padding: 10, marginTop: StatusBar.currentHeight || 0 }}>
            <ScrollView>
                <View style={{ alignItems: 'center', marginBottom: 20, flexDirection: 'row' }}>
                    <Pressable onPress={() => navigation.goBack()}>
                        <MaterialIcons name='arrow-back' size={20} style={{ justifyContent: 'flex-start' }} />
                    </Pressable>
                    <Text style={{ fontWeight: '900', fontSize: 19, alignSelf: 'center', marginLeft: 20 }}>{productDetails[0]?.product_name}</Text>
                </View>
                <View>
                    {
                        productDetails[0]?.images.length > 0 ?
                            <View>
                                {productDetails[0]?.images.map((imgSrc, index) => (
                                    <View>
                                        <Image
                                            key={item => item.image_id.toString() || index.toString()}
                                            source={{ uri: `data:image/jpeg;base64,${imgSrc.image_data}` }}
                                        />
                                        {/* <Text>{JSON.stringify(imgSrc)}</Text> */}
                                    </View>
                                ))}
                            </View>
                            :
                            <View style={{ justifyContent: 'center', alignItems: 'center', height: 200, backgroundColor: '#d4d2d2ff', borderRadius: 10, marginBottom: 20 }}>
                                <Text style={{ fontSize: 20, fontWeight: 'thin' }}>No Image Available</Text>
                            </View>

                    }
                </View >
                <View>
                    {/* <Text>{JSON.stringify(productDetails, null, 2)}</Text> */}
                    <View style={{ flexDirection: 'row', justifyContent: 'space-between', marginVertical: 10, marginHorizontal: 5 }}>
                        <Text style={{ fontSize: 19, fontWeight: '900' }}>Rs.{productDetails[0]?.price || 'N/A'}</Text>
                        <Text style={{ fontSize: 19, fontWeight: '500' }}>Stock Quantity: {productDetails[0]?.stockQuantity || 'N/A'}</Text>
                    </View>
                    <View>
                        <Text style={{ fontWeight: 'bold', fontSize: 18 }}>
                            {productDetails[0]?.category_name || 'N/A'}
                        </Text>
                        <Text style={{ fontWeight: 'bold', fontSize: 18 }}>Description</Text>
                        <Text style={{ fontSize: 16 }}>{productDetails[0]?.description || 'N/A'}</Text>
                    </View>
                </View>
                <View>
                    {productDetails[0]?.reviews && productDetails[0]?.reviews.length > 0 ? (
                        productDetails[0]?.reviews.map((review, index) => (
                            <View key={index} style={{ justifyContent: 'flex-start', height: 80, flexDirection: 'row', marginTop: 10, padding: 10, borderWidth: 1, borderColor: '#ccc', borderRadius: 5 }}>
                                <Image
                                    source={{ uri: review.profile_image ? `data:image/png;base64,${review.profile_image}` : 'https://www.gravatar.com/avatar/00000000000000000000000000000000?d=mp&f=y' }}
                                    style={{ width: 50, height: 50, borderRadius: 50 }} />
                                <View style={{ marginLeft: 30 }}>
                                    <Text style={{ fontWeight: 'bold' }}>{review.full_name || 'Anoymous'}</Text>
                                    <Text style={{ fontWeight: 'bold' }}>{review.email || 'N/A'}</Text>
                                    <Text>Rating: {review.rating || 'N/A'}</Text>
                                    <Text>{review.comment || ''}</Text>
                                </View>
                            </View>
                        ))
                    ) : (
                        <Text>No Reviews Available</Text>
                    )}
                </View>
            </ScrollView>
        </SafeAreaView >
    )
}