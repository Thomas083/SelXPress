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
        <div v-for="comment in filteredComments">
            <div class="review-img-editor-container">
                <img src="@/assets/Product/client.png" alt="editor" />
                <h3>{{ comment.user.username }}</h3>
            </div>
            <div class="star-rating-container review">
                <div v-for="index  in comment.mark.rate" :key="index">
                    <img src="../../assets/Product/yellow_star.png" alt="yellow stars" />
                </div>
                <div v-for="index in (5 - comment.mark.rate)" :key="index">
                    <img src="../../assets/Product/star.png" alt="stars" />
                </div>
                <div class="review-title">{{ comment.title }}</div>
            </div>
            <div class="review_publication_date">Published on: {{ formatCreatedAt(comment.createdAt) }}</div>
            <div class="review-comment">{{ comment.message }}</div>
        </div>
    </div>
</template>

<script>
import { GET, POST } from '@/api/axios';
import { ENDPOINTS } from "@/api/endpoints";
import { createToast } from 'mosha-vue-toastify';
import InputComponent from "@/components/global/InputComponent.vue";
import { format } from 'date-fns';

export default {
    name: 'CustomerReview',
    components: {
        InputComponent,
    },
    props: {
        comments: {
            type: Array,
            required: true
        },
        productId: {
            type: Number,
            required: true,
        }
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
            hoveredStars: 0,
        }
    },
    computed: {
        filteredComments() {
            const commentCopy = [...this.comments]
            commentCopy.sort((a,b) => new Date(b.createdAt) - new Date(a.createdAt)) 
            return commentCopy
        }
    },
    methods: {
        setHoveredStars(index) {
            this.hoveredStars = index;
        },
        updateData(e, key) {
            this.sendReviewData = Object.assign(this.sendReviewData, { [key]: e });
        },
        addReview() {
            this.sendReviewData.rate = this.hoveredStars;
            this.sendReviewData.productId = this.productId;
            if (!localStorage.getItem('user')) createToast(`You need to be connected to add a review on this product`, { type: 'danger', position: 'bottom-right' })
            else if (this.sendReviewData.rate == 0) createToast(`You need to select at least one star please !`, { type: 'danger', position: 'bottom-right' })
            else {
                GET(ENDPOINTS.GET_ONE_USER, JSON.parse(localStorage.getItem('user')).token)
                .then((response) => {
                    this.sendReviewData.userId = response.data.id
                    POST(ENDPOINTS.CREATE_COMMENT, this.sendReviewData, JSON.parse(localStorage.getItem('user')).token)
                        .then(() => {
                            createToast({ title: 'Comment created successfully', description: `Your ${this.sendReviewData.title} comment is sucessfully created` }, { type: 'success', position: 'bottom-right' })
                            window.location.reload()
                        })
                        .catch(() => {
                            createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' })
                        });
                })
                .catch((error) => {
                    console.dir(error)
                });
            }
        },
        formatCreatedAt(createdAt) {
            if (createdAt) return format(new Date(createdAt), 'dd/MM/yyyy');
            else return '';
        },
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