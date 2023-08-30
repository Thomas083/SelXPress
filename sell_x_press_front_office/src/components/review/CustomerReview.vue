<template>
    <div class="review-container">
        <input-component id="input-title" name="input-title" class="add-review-title" type="text"
            placeholder="Enter your title..." @input="updateData($event, 'title')" />
        <textarea class="add-review" rows="5" id="input-description" name="input-description"
            placeholder="Write your review here..." @change="updateData($event.target.value, 'message')"></textarea>
        <div class="add-review-container">
            <div v-for="index in 5" :key="index">
                <img v-if="index <= hoveredStars" src="../../assets/Product/yellow_star.png" alt="yellow stars"
                    @mouseover="hoveredStars = index" :style="{ cursor: 'pointer' }" @click="setHoveredStars(index)" />
                <img v-else src="../../assets/Product/star.png" alt="stars" @mouseover="hoveredStars = index"
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
                    <img src="../../assets/Product/yellow_star.png" alt="yellow stars" />
                </div>
                <div v-for="index in (5 - review.rating)" :key="index">
                    <img src="../../assets/Product/star.png" alt="stars" />
                </div>
                <div class="review-title">{{ review.title }}</div>
            </div>
            <div class="review_publication_date">Published on: {{ review.publication_date }}</div>
            <div class="review-comment">{{ review.comment }}</div>
        </div>
    </div>
</template>

<script>
import { POST } from '@/api/axios';
import { ENDPOINTS } from "@/api/endpoints";
import { createToast } from 'mosha-vue-toastify';
import InputComponent from "@/components/global/InputComponent.vue";

export default {
    name: 'CustomerReview',
    components: {
        InputComponent,
    },
    data() {
        return {
            sendReviewData: {
                title: '',
                message: '',
                userId: null,
                productId: null,
                rate: 0,
            },
            
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
        }
    },
    methods: {
        addReview() {
            this.sendReviewData.rate = this.hoveredStars;
            if (this.sendReviewData.rate === 0) createToast(`You need to select at least one star please !`, { type: 'danger', position: 'bottom-right' })
            else {
                POST(ENDPOINTS.CREATE_COMMENT, this.sendReviewData, JSON.parse(localStorage.getItem('user')).token)
                    .then(() => {
                        createToast({ title: 'Comment created successfully', description: `Your ${this.sendReviewData.title} comment is sucessfully created` }, { type: 'success', position: 'bottom-right' })
                    })
                    .catch(() => {
                        createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' })
                    });
            }
        }
    },
}
</script>

<style scoped>
.review_publication_date {
    color: var(--main-grey-separation);
    text-align: start;
}

.add-review-btn {
    margin-left: 2rem;
    padding-left: 1rem;
    padding-right: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
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

.review-img-editor-container {
    margin-top: 1rem;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: flex-end;
    gap: 1rem;
}

.add-review-container {
    display: flex;
    flex-direction: row;
    gap: 0.5rem;
    margin-top: 0.5rem;
}

img {
    height: 3rem;
}

.star-rating-container {
    display: flex;
    flex-wrap: wrap;
    gap: 0.5rem;
    align-items: flex-end;
}

.star-rating-container img {
    height: 1rem;
}
</style>