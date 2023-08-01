<template>
    <div class="product-details-container">
        <div class="img-details-container">
            <div class="img-publication-container">
                <div class="img-upload" id="input-upload-file" name="input-upload-file">
                    <img :src="image" alt="Product Image" />
                </div>
                <p class="publication_date">Published on: {{ publication_date }}</p>
            </div>
            <div class="product-details">
                <h2 class="product-title">{{ title }}</h2>
                <div class="product-seller">Sold by: <span class="product-seller-name">{{ seller }}</span></div>
                <div class="product-price-cart-container">
                    <div class="product-price">
                        <h2 class="price">{{ price }} €</h2>
                        <div>All prices include VAT</div>
                    </div>
                    <button class="btn btn-primary add-to-cart-btn" @click="addToCart()">
                        <span>Add to cart</span>
                        <img src="../assets/Product/add-to-cart.png" alt="Add to cart" />
                    </button>
                </div>
                <div v-for="(attribute, index) in attributes" :key="index" class="attributes-container">
                    <div class="attribute-name">{{ attribute.name }}:</div>
                    <div v-if="attribute.name === 'color'" class="attributes-container">
                        <div class="color-attribute" v-for="(data, index) in attribute.data"
                            :class="{ 'color-attribute-selected': isAttributeSelected(attribute.name, data.value) }"
                            @click="setAttribute(attribute.name, data.value)" :style="{ backgroundColor: data.value }">
                        </div>
                    </div>
                    <div v-else class="attributes-container">
                        <div class="attribute" v-for="(data, index) in attribute.data"
                            :class="{ 'attribute-selected': isAttributeSelected(attribute.name, data.value) }"
                            @click="setAttribute(attribute.name, data.value)">
                            {{ data.value }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="separation-line"></div>
        <div class="description-container">
            <h1>Description</h1>
            <p>{{ description }}</p>
        </div>
        <div class="separation-line"></div>
        <div class="customer-review-container">
            <h1>Customer Review</h1>
            <div class="rating-review-container">
                <div class="rating-container">
                    <div class="star-rating-container">
                        <div v-for="index  in product_rating" :key="index">
                            <img src="../assets/Product/yellow_star.png" alt="yellow stars" />
                        </div>
                        <div v-for="index in (5 - product_rating)" :key="index">
                            <img src="../assets/Product/star.png" alt="stars" />
                        </div>
                        <span class="rating">{{ product_rating }} out of 5</span>
                    </div>
                    <div class="number-review">{{ number_review }} customer review</div>
                    <div v-for="(value, index) in 5" class="ratings-table">
                        <span>{{ 5 - (index) }} star</span>
                        <div class="level-rating">
                            <div class="level-rating"
                                :style="{ backgroundColor: '#F17720', width: Math.floor((star_rating[5 - index] / (number_review)) * 100) + '%' }">
                            </div>
                        </div>
                        <span>{{ Math.floor((star_rating[5 - index] / (number_review)) * 100) }}%</span>
                    </div>
                </div>
                <div class="review-container">
                    <input-component id="input-title" name="input-title" class="add-review-title" type="text"
                        placeholder="Enter your title..." @input="updateData($event, 'title')" />
                    <textarea class="add-review" rows="5" cols="110" id="input-description" name="input-description"
                        placeholder="Write your review here..."
                        @change="updateData($event.target.value, 'message')"></textarea>
                    <div class="add-review-container">
                        <div v-for="index in 5" :key="index">
                            <img v-if="index <= hoveredStars" src="../assets/Product/yellow_star.png" alt="yellow stars"
                                @mouseover="hoveredStars = index" :style="{ cursor: 'pointer' }"
                                @click="setHoveredStars(index)" />
                            <img v-else src="../assets/Product/star.png" alt="stars" @mouseover="hoveredStars = index"
                                :style="{ cursor: 'pointer' }" @click="setHoveredStars(index)" />
                        </div>
                        <button class="btn btn-primary add-review-btn" @click="addReview()">Add your review</button>
                    </div>
                    <div v-for="(review, index) in reviews">
                        <div class="review-img-editor-container">
                            <img src="@/assets/Product/client.png" alt="editor" />
                            <h3>{{ review.editor }}</h3>
                        </div>
                        <div class="star-rating-container review">
                            <div v-for="index  in review.rating" :key="index">
                                <img src="../assets/Product/yellow_star.png" alt="yellow stars" />
                            </div>
                            <div v-for="index in (5 - review.rating)" :key="index">
                                <img src="../assets/Product/star.png" alt="stars" />
                            </div>
                            <div class="review-title">{{ review.title }}</div>
                        </div>
                        <div class="review_publication_date">Published on: {{ review.publication_date }}</div>
                        <div class="review-comment">{{ review.comment }}</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import InputComponent from "@/components/global/InputComponent.vue";

export default {
    name: 'ProductDetailsView',
    components: {
        InputComponent,
    },
    data() {
        return {
            addProduct: {
                attributes: [],
            },
            title: 'ARTINABS Ocean Wave Simulation LED Projector Lamp',
            seller: 'ARTINABS',
            price: '25,99',
            image: '',
            attribute_selected: [
                { name: 'color', selected: false },
                { name: 'size', selected: false }
            ],
            attributes: [
                {
                    name: 'color',
                    data: [
                        { 'name': 'blue', 'value': '#0000FF' },
                        { 'name': 'red', 'value': '#FF0000' },
                        { 'name': 'green', 'value': '#00FF00' }
                    ]
                },
                {
                    name: 'size',
                    data: [
                        { 'name': 'extra_small', 'value': 'xs' },
                        { 'name': 'small', 'value': 's' },
                        { 'name': 'medium', 'value': 'm' },
                        { 'name': 'large', 'value': 'l' },
                        { 'name': 'extra_large', 'value': 'xl' }
                    ]
                },
            ],
            description: 'ARTINABS Ocean Wave Simulation LED Projector Lamp, Child Night Light with 8 Color Modes 6 Music Sounds Remote Control Timer Bedside Lamp for Decoration Baby Room (White)',
            publication_date: '20/06/2023',
            number_review: '430',
            product_rating: 4,
            star_rating: {
                5: 270,
                4: 100,
                3: 30,
                2: 0,
                1: 30,
            },
            hoveredStars: 0,
            reviews: [
                {
                    title: 'Awesome product !',
                    editor: 'Elsharion',
                    rating: 4,
                    comment: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Assumenda obcaecati sunt quasi odit labore commodi fugit ducimus nostrum asperiores aperiam laboriosam, ab repellat dolore, nisi fuga? Iure, obcaecati. Repellendus, beatae?',
                    publication_date: '20/06/2023',
                },
                {
                    title: 'Terrible.',
                    editor: 'Thomas',
                    rating: 2,
                    comment: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Assumenda obcaecati sunt quasi odit labore commodi fugit ducimus nostrum asperiores aperiam laboriosam, ab repellat dolore, nisi fuga? Iure, obcaecati. Repellendus, beatae?',
                    publication_date: '22/06/2023',
                },
            ],
            sendReviewData: {
                title: '',
                message: '',
                author: '',
                rating: 0,
            }
        }
    },
    methods: {
        setAttribute(attribute_name, attribute_value) {
            const existingAttributeIndex = this.addProduct.attributes.findIndex(
                (data) => data.name === attribute_name
            );

            if (existingAttributeIndex !== -1) {
                if (this.addProduct.attributes[existingAttributeIndex].value === attribute_value) {
                    this.addProduct.attributes.splice(existingAttributeIndex, 1);
                    this.attribute_selected[attribute_name] = false;
                } else {
                    this.addProduct.attributes[existingAttributeIndex].value = attribute_value;
                }
            } else {
                this.addProduct.attributes.push({ name: attribute_name, value: attribute_value });
            }

            this.attribute_selected[attribute_name] = true;
        },

        isAttributeSelected(attribute_name, attribute_value) {
            return (
                this.attribute_selected[attribute_name] &&
                this.addProduct.attributes.some(
                    (data) => data.name === attribute_name && data.value === attribute_value
                )
            );
        },
        setHoveredStars(index) {
            this.hoveredStars = index;
        },
        updateData(e, key) {
            this.sendReviewData = Object.assign(this.sendReviewData, { [key]: e });
        },
        addToCart() {
            console.dir(this.addProduct)
        },
        addReview() {
            this.sendReviewData.rating = this.hoveredStars;
        }
    }
}

</script>

<style scoped>
.product-details-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 3rem;
    margin: 1rem;
    background-color: var(--main-white);
    border-radius: 1rem;
}

.img-details-container {
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    gap: 1rem;
}

.img-publication-container {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.publication_date {
    text-align: end;
    margin-right: 0.5rem;
    color: var(--main-grey-separation);
}

.review_publication_date {
    color: var(--main-grey-separation);
    text-align: start;
}

.separation-line {
    border: 1px var(--main-grey-separation) solid;
    width: 100%;
}

.product-details {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
    padding-left: 3rem;
    width: 70%;
}


.description-container {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 0.5rem;
    width: 70%;
    word-wrap: break-word;
    text-align: justify;
}

.attribute:hover,
.attribute-selected {
    background-color: var(--main-blue);
}

.img-upload {
    width: 435px;
    height: 436px;
    border-radius: 1rem;
    border: 1px var(--main-black) solid;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.img-upload img {
    max-width: 100%;
    max-height: 100%;
    border-radius: 1rem;
}

.upload-file {
    display: flex;
    flex-direction: row;
    align-items: baseline;
    justify-content: flex-end;
    margin-top: 0.5rem;
    padding-right: 1rem;
    gap: 1rem;
    width: 100%;
    cursor: pointer;
}

.upload-file p {
    font-size: 15px;
    color: var(--main-black);
    font-weight: bold;
}

.upload-file p:hover {
    text-decoration: underline;
}

.upload-file img {
    height: 2rem;
}

.product-price-cart-container {
    display: flex;
    flex-direction: row;
    gap: 3rem;
}

.product-title {
    font-size: 2rem;
    word-wrap: break-word;
    text-align: justify;
}

.product-seller {
    color: var(--main-grey-separation);
    font-size: 1.3rem;
}

.product-seller-name {
    text-decoration: underline;
}

.product-price {
    text-align: start;
}

.price {
    font-size: 3.5rem;
}

.attributes-container {
    display: flex;
    flex-direction: row;
    gap: 1.5rem;
    align-items: center;
    justify-content: center;
}

.attribute-name {
    font-size: 3rem;
}

.add-to-cart-btn {
    padding-left: 1rem;
    padding-right: 1rem;
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 1rem;
    height: 10vh;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.add-review-btn {
    margin-left: 2rem;
    padding-left: 1rem;
    padding-right: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

img {
    height: 3rem;
}

.attribute {
    cursor: pointer;
    border-radius: 0.5rem;
    border: 1px solid var(--main-black);
    padding: 1rem;
    text-transform: uppercase;
}

.attribute-selected {
    background-color: var(--main-blue);
}

.color-attribute {
    cursor: pointer;
    border: 1px solid var(--main-black);
    width: 7vw;
    height: 5vh;
    border-radius: 10px;
}

.color-attribute-selected {
    border: 2px solid var(--main-blue);
}

.customer-review-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    align-items: flex-start;
}

.rating-review-container {
    display: flex;
    flex-direction: row;
    gap: 3rem;
}

.rating-container {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 0.5rem;
}

.number-review {
    color: var(--main-grey-separation);
    font-size: 1rem;
}

.star-rating-container {
    display: flex;
    gap: 0.5rem;
    align-items: flex-end;
}

.star-rating-container img {
    height: 2rem;
}

.ratings-table {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 1rem;
}

.level-rating {
    border: none;
    background-color: var(--main-grey-separation);
    height: 2vh;
    width: 12vw;
}

.rating {
    margin-left: 1rem;
    font-size: 1.5rem;
}

.add-review-container {
    display: flex;
    flex-direction: row;
    gap: 0.5rem;
    margin-top: 0.5rem;
}

.add-review {
    border-radius: 1rem;
    padding: 1rem;
    border-color: var(--main-black);
}

.review-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.review-img-editor-container {
    margin-top: 1rem;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: flex-end;
    gap: 1rem;
}

.review img {
    height: 1rem;
}

.review-title {
    font-weight: bold;
    font-size: 0.8rem;
}

.review-comment {
    margin-top: 1rem;
    text-align: justify;
}

.add-review-title {
    border-radius: 1rem;
    padding: 0.5rem;
}
</style>